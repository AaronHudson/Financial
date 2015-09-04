using Financial.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("StartPage");

            return View();
        }

        public ActionResult StartPage()
        {
            ApplicationUser user = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            // ViewBag.Title = budget.Title;
            List<Category> categories = user.Categories.ToList();
            return View(categories);
        }

        public ActionResult Category(int Id)
        {
            Category category = db.Budgets.Find(Id);
            ViewBag.Title = category.Title;
            return View(category.Transactions.ToList());
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}