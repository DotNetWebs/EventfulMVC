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
    /// Summary description for Product
    /// </summary>
    public class Product
    {
        public Product()
        {
        }

        public Product(int id, string name)
        {
            this.productId = id;
            this.productName = name;
        }

        private int productId;
        public int ProductId
        {
            get
            {
                return productId;
            }
            set
            {
                productId = value;
            }
        }

        private string productName;
        public string ProductName
        {
            get
            {
                return productName;
            }
            set
            {
                productName = value;
            }
        }
    }
}