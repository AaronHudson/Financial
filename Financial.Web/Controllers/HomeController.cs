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
            return View();
        }

        public ActionResult StartPage()
        {
            FinancialtronUser user = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            List<Budget> budgets = user.Budgets.ToList();
            return View(budgets);
        }

        public ActionResult AddTransaction()
        {
            return View();
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