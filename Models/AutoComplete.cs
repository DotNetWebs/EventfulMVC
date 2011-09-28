using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ShoppersGuide;

namespace EventfulMVC.Models
{
    public class AutoComplete
    {
        internal List<LazyList> AutoCompleteBranch(string searchText, int maxResults)
        {
            int count = maxResults;
            var dDb = new DirectoryDb();
            List<Branch> branches = dDb.GetAutoCompleteStore(searchText).Take(count).ToList();
            return branches.Select(t => new LazyList(t.BusinessName, t.BusinessName)).ToList();
        }

        internal List<LazyList> AutoCompleteBrand(string searchText, int maxResults)
        {
            int count = maxResults;
            var dDb = new DirectoryDb();
            List<Brand> brands = dDb.GetAutoCompleteBrand(searchText).Take(count).ToList();
            return brands.Select(t => new LazyList(t.BrandName, t.BrandName)).ToList();
        }

        internal List<LazyList> AutoCompleteProduct(string searchText, int maxResults)
        {
            int count = maxResults;
            var dDb = new DirectoryDb();
            List<Product> products = dDb.GetAutoCompleteProduct(searchText).Take(count).ToList();
            return products.Select(t => new LazyList(t.ProductName, t.ProductName)).ToList();
        }
    }

    internal class LazyList
    {
        public LazyList(string label, string value)
        {
            Label = label;
            Value = Underscore(value);
        }

        public string Label { get; set; }
        public string Value { get; set; }

        public static string Underscore(string value)
        {
            var rx = new Regex(@" ");

            if (rx.IsMatch(value))
            {
                return rx.Replace(value, "_");
            }

            return value;
        }
    }
}