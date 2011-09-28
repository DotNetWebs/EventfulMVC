using System.Collections.Generic;
using ShoppersGuide;

namespace EventfulMVC.Models
{
    /// <summary>
    /// Summary description for Branch
    /// </summary>
    public class Branch : Business
    {
        public Branch()
        {
        }

        public Branch(int branchId, string businessName, string zoneName)
        {
            BranchID = branchId;
            BusinessName = businessName;
            ZoneName = zoneName;
        }

        public Branch(int branchId, int businessID, string businessName, int location, string locationName)
        {
            BranchID = branchId;
            BusinessID = businessID;
            BusinessName = businessName;
            Location = location;
            LocationName = locationName;
        }

        public int BranchID { get; set; }

        public int Count { get; set; }

        public int Location { get; set; }

        public string LocationName { get; set; }

        public int Zone { get; set; }

        public string ZoneName { get; set; }

        public string ZoneImageURL { get; set; }

        public string BranchImageURL { get; set; }

        public string FormalName { get; set; }

        public string ContactName { get; set; }

        public string EMail { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string PostCode { get; set; }

        public string Telephone { get; set; }

        public string UserContent { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int MapZoom { get; set; }

        public string WebsiteURL { get; set; }

        public string AdditionalURL { get; set; }

        public string AdditionalURL2 { get; set; }

        public string AdditionalURL3 { get; set; }

        public string AdditionalURL4 { get; set; }

        public int RightmoveID { get; set; }

        public string TwitterID { get; set; }

        public string StrapLine { get; set; }

        public bool Suspended { get; set; }

        public bool Seniors { get; set; }

        public bool Children { get; set; }

        public bool Disabled { get; set; }

        public bool FairTrade { get; set; }

        public bool LateOpening { get; set; }

        public bool SundayOpening { get; set; }

        public bool Affiliate { get; set; }

        public bool EventParticipant { get; set; }

        public bool Hbis { get; set; }

        public bool ResidentContent { get; set; }

        public bool Blog { get; set; }

        public bool Twitter { get; set; }

        public List<Category> Categories { get; set; }

        public int? HomePageProducts { get; set; }

        public int? HomePageBrands { get; set; }

        public BusinessType Type { get; set; }

        public enum BusinessType
        {
            Default,
            Retail,
            Restaurant,
            PropertyAgent,
            RetailWithRestaurant,
            Venue
        }

        public int Likes { get; set; }
    }

}
