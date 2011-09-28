using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Microsoft.Security.Application;

namespace EventfulMVC.Models
{
    public class Core
    {
        private const string Endpoint = "http://api.eventful.com/rest/events/search?&app_key=ZWfsHsT568vk7JFr&location=";

        private const string EventfulMode = "&date=Future";

        public List<Event> GetEvents()
        {
            List<Event> events;

            const string key = "ModelsCoreGetEvents";

            if (!CacheHelper.Get(key, out events))
            {
                var dDb = new DirectoryDb();
                List<Venue> venues = dDb.GetAllVenues();
                events = GetVenueEvents(venues);
                CacheHelper.Add(events, key);
            }

            IEnumerable<Event> result = events.Where(s => s.StartTime > DateTime.Now).OrderBy(s => s.StartTime);

            return result.ToList();
        }

        private static List<Event> GetVenueEvents(IEnumerable<Venue> venues)
        {
            var events = new List<Event>();

            foreach (Venue venue in venues)
            {
                var request = WebRequest.Create(Endpoint + venue.EventfulId + EventfulMode) as HttpWebRequest;

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
                                    XElement eventId = item.Element("title");
                                    XElement eventUrl = item.Element("url");
                                    XElement eventDescription = item.Element("description");
                                    XElement eventOwner = item.Element("owner");
                                    XElement eventStartTime = item.Element("start_time");
                                    XElement eventRecurString = item.Element("recur_string");
                                    var eventfulEvent = new Event();
                                    if (eventId != null) eventfulEvent.Title = eventId.Value;
                                    if (eventUrl != null) eventfulEvent.Url = eventUrl.Value;
                                    if (eventDescription != null)
                                        eventfulEvent.Description = Sanitizer.GetSafeHtmlFragment(eventDescription.Value);
                                    if (eventOwner != null) eventfulEvent.Owner = eventOwner.Value;
                                    if (eventStartTime != null)
                                        eventfulEvent.StartTime = DateTime.Parse(eventStartTime.Value);
                                    if (eventRecurString != null) eventfulEvent.RecurString = eventRecurString.Value;
                                    eventfulEvent.Venue = venue;
                                    events.Add(eventfulEvent);
                                }
                            }
                        }
                    }
            }

            return events;
        }
    }
}