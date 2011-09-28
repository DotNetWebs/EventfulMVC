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
/// Summary description for Brand
/// </summary>
namespace ShoppersGuide
{
    /// <summary>
    /// Summary description for Product
    /// </summary>
    public class Brand
    {
        public Brand()
        {
        }

        public Brand(int id, string name)
        {
            this.brandId = id;
            this.brandName = name;
        }

        private int brandId;
        public int BrandId
        {
            get
            {
                return brandId;
            }
            set
            {
                brandId = value;
            }
        }

        private string brandName;
        public string BrandName
        {
            get
            {
                return brandName;
            }
            set
            {
                brandName = value;
            }
        }
    }
}
