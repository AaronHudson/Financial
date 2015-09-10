using Financial.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            List<Budget> budgets = user.Budgets.ToList();
            return View(budgets);
        }

        public ActionResult Category(int Id)
        {
            Category category = db.Categories.Find(Id);
            return View(category.Transactions.ToList());
        }
        
        public ActionResult ShareBudget(string[] userNames, int budgetId)
        {
            foreach (var userName in userNames)
            {
                db.Users.FirstOrDefault(user => user.UserName == userName).Budgets.Add(db.Budgets.Find(budgetId));
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}