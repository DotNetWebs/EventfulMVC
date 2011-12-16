using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventfulMVC.Models
{
    public class VenueOwner
    {

        public VenueOwner(string venueName, string ownerName)
        {
            this.VenueName = venueName;
            this.OwnerName = ownerName;
        }
        
        public string VenueName { get; set; }
        public string OwnerName { get; set; }
    }
}