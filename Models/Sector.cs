using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Sector
/// </summary>
namespace ShoppersGuide
{
    /// <summary>
    /// Summary description for Sector
    /// </summary>
    public class Sector
    {
        public Sector()
        {
        }

        public Sector(int sectorID, string sectorName)
        {
            this.sectorID = sectorID;
            this.sectorName = sectorName;
        }

        protected int sectorID;
        public int SectorID
        {
            get
            {
                return sectorID;
            }
            set
            {
                sectorID = value;
            }
        }

        protected string borderColor;
        public string BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
            }
        }

        protected string color;
        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        protected string sectorName;
        public string SectorName
        {
            get
            {
                return sectorName;
            }
            set
            {
                sectorName = value;
            }
        }

        protected string sectorImageURL;
        public string SectorImageURL
        {
            get
            {
                return sectorImageURL;
            }
            set
            {
                sectorImageURL = value;
            }
        }

        protected int featuredBranch;
        public int FeaturedBranch
        {
            get
            {
                return featuredBranch;
            }
            set
            {
                featuredBranch = value;
            }
        }

        protected int featuredLocation;
        public int FeaturedLocation
        {
            get
            {
                return featuredLocation;
            }
            set
            {
                featuredLocation = value;
            }
        }
    }
}
