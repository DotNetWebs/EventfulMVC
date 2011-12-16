using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ShoppersGuide
{
    /// <summary>
    /// Summary description for Subscription
    /// </summary>
    public class Subscription
    {
        public Subscription()
        {
        }
        public int SubscriptionID { get; set; }
        public string BusinessName { get; set; }
        public int Branch { get; set; }
        public DateTime FirstSubscribed { get; set; }
        public DateTime NextRenewal { get; set; }
        public bool StandingOrder { get; set; }
        public bool StandingOrderActive { get; set; }
    }
}
