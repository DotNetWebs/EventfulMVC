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
    /// Summary description for Category
    /// </summary>
    public class Category : ShoppersGuide.Sector
    {
        public Category()
        {
        }
        
        public Category(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        private string imageURL;
        public string ImageURL
        {
            get
            {
                return imageURL;
            }
            set
            {
                imageURL = value;
            }
        }

        private int branches;
        public int Branches
        {
            get
            {
                return branches;
            }
            set
            {
                branches = value;
            }
        }

    }
}
