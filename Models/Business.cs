using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ShoppersGuide
{
/// <summary>
/// Summary description for Business
/// </summary>
    public class Business
    {
        public Business()
        {
        }

        public Business(int businessID, string businessName)
        {
            this.businessID = businessID;
            this.businessName = businessName;
        }

        protected int businessID;
        public int BusinessID
        {
            get
            {
                return businessID;
            }
            set
            {
                businessID = value;
            }
        }

        protected string businessName;
        public string BusinessName
        {
            get
            {
                return businessName;
            }
            set
            {
                businessName = value;
            }
        }
    }
}
