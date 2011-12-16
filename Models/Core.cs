using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.Security.Application;

namespace EventfulMVC.Models
{
    public class Core
    {
        private const string Endpoint = "http://api.eventful.com/rest/events/search?&app_key=ZWfsHsT568vk7JFr&location=";
        private const string EventfulMode = "&date=Future&include=instances";

        public List<Event> GetEvents()
        {
            List<Event> events;

            const string key = "ModelsCoreGetEvents";

            if (!CacheHelper.Get(key, out events))
            {
                var dDb = new DirectoryDb();
                List<Venue> venues = dDb.GetAllVenues();

                List<VenueOwner> owners = dDb.GetGetVenueOwners();

                events = GetVenueEvents(venues, owners);
                CacheHelper.Add(events, key);
            }

            IEnumerable<Event> result = events.Where(s => s.StartTime > DateTime.Now).OrderBy(s => s.StartTime);

            return result.ToList();
        }

        private static List<Event> GetVenueEvents(IEnumerable<Venue> venues, List<VenueOwner> owners)
        {
            var events = new List<Event>();

            foreach (Venue venue in venues)
            {
                var request = WebRequest.Create(Endpoint + venue.EventfulId + EventfulMode + "&page_size=50") as HttpWebRequest;

                if (request != null)
                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response != null)
                        {
                            // ReSharper disable AssignNullToNotNullAttribute
                            var reader = new StreamReader(response.GetResponseStream());
                            // ReSharper restore AssignNullToNotNullAttribute
                            string cpFile = reader.ReadToEnd();
                            XDocument cpXdoc = XDocument.Parse(cpFile);
                            XElement xElement = cpXdoc.Element("search");

                            if (xElement != null)
                            {
                                IEnumerable<XElement> elements = xElement.Elements("events").Elements("event");

                                foreach (XElement item in elements)
                                {
                                    XElement eventOwner = item.Element("owner");
                                    bool isOwner = owners.Any(ov => eventOwner != null && (ov.OwnerName == eventOwner.Value && ov.VenueName == venue.EventfulId));

                                    if (isOwner)
                                    {

                                        XElement eventId = item.Element("title");
                                        XElement eventUrl = item.Element("url");
                                        XElement eventDescription = item.Element("description");
                                        XElement eventStartTime = item.Element("start_time");
                                        XElement eventRecurString = item.Element("recur_string");
                                        XElement eventImage = null;
                                        
                                        var element = item.Element("image");

                                        if (element != null)
                                        {
                                            var xElement1 = element.Element("medium");
                                            if (xElement1 != null)
                                            {
                                                eventImage = xElement1.Element("url");
                                            }
                                        }

                                        var eventfulEvent = new Event();

                                        if (eventId != null) eventfulEvent.Title = eventId.Value;
                                        if (eventUrl != null) eventfulEvent.Url = eventUrl.Value;
                                        if (eventImage != null) eventfulEvent.Image = eventImage.Value;
                                        if (eventDescription != null)
                                            eventfulEvent.Description =
                                                Sanitizer.GetSafeHtmlFragment(eventDescription.Value.Replace("\n","<br/>"));
                                        if (eventOwner != null) eventfulEvent.Owner = eventOwner.Value;
                                        if (eventStartTime != null)
                                            eventfulEvent.StartTime = DateTime.Parse(eventStartTime.Value);
                                        eventfulEvent.Venue = venue;

                                        if (eventRecurString != null && eventRecurString.Value != "")
                                        {
                                            eventfulEvent.RecurString = eventRecurString.Value;
                                            const string expression = @"(?<=daily until ).+";

                                            var rx = new Regex(expression);

                                            if (rx.IsMatch(eventfulEvent.RecurString))
                                            {
                                                string endDateString = rx.Match(eventfulEvent.RecurString).Value;
                                                DateTime endDate = DateTime.Parse(endDateString);
                                                TimeSpan span = endDate.Date.Subtract(eventfulEvent.StartTime.Date);
                                                int furtherPerformances = span.Days;
                                                eventfulEvent.Repeat = Event.RepeatType.Daily;

                                                for (int i = 0; i < furtherPerformances; i++)
                                                {
                                                    Event newEvent = eventfulEvent.AddRepeat();
                                                    newEvent.StartTime = newEvent.StartTime.AddHours((i + 1) * 24);
                                                    newEvent.Description = "See orginal perfomance for description";
                                                    events.Add(newEvent);
                                                }
                                            }

                                            const string expression2 = @"(?<=weekly on [a-z]+ until ).+";

                                            var rx2 = new Regex(expression2, RegexOptions.IgnoreCase);

                                            if (rx2.IsMatch(eventfulEvent.RecurString))
                                            {
                                                string endDateString = rx2.Match(eventfulEvent.RecurString).Value;
                                                DateTime endDate = DateTime.Parse(endDateString);
                                                TimeSpan span = endDate.Date.Subtract(eventfulEvent.StartTime.Date);
                                                int furtherPerformances = span.Days / 7;
                                                eventfulEvent.Repeat = Event.RepeatType.Weekly;

                                                for (int i = 0; i < furtherPerformances; i++)
                                                {
                                                    Event newEvent = eventfulEvent.AddRepeat();
                                                    newEvent.StartTime = newEvent.StartTime.AddHours((i + 1) * 24 * 7);
                                                    newEvent.Description = "See orginal perfomance for description";
                                                    events.Add(newEvent);
                                                }
                                            }
                                        }
                                        events.Add(eventfulEvent);
                                    }
                                }
                            }
                        }
                    }
            }

            return events;
        }
    }
}