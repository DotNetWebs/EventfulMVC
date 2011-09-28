using System.Collections.Generic;
using System.Web.Mvc;
using EventfulMVC.Models;

namespace EventfulMVC.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var core = new Core();
            List<Event> model = core.GetEvents();
            return View(model);
        }

        [HttpPost]
        public JsonResult AutoCompleteBranches(string searchText, int maxResults)
        {
            var branches = new AutoComplete();
            var result = branches.AutoCompleteBranch(searchText, maxResults);
            return Json(result);
        }

        [HttpPost]
        public JsonResult AutoCompleteBrand(string searchText, int maxResults)
        {
            var brands = new AutoComplete();
            var result = brands.AutoCompleteBrand(searchText, maxResults);
            return Json(result);
        }

        [HttpPost]
        public JsonResult AutoCompleteProduct(string searchText, int maxResults)
        {
            var products = new AutoComplete();
            var result = products.AutoCompleteProduct(searchText, maxResults);
            return Json(result);
        }

    }
}
