﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventfulMVC.Models
{
    public class Event
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public string RecurString { get; set; }
        public DateTime StartTime { get; set; }
        public Venue Venue { get; set; }
    }
}