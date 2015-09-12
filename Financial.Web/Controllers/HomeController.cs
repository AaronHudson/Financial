using Financial.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using SendGrid;

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

        public ActionResult CategoryAmounts(int Id)
        {
            Budget budget = db.Budgets.Find(Id);
            List<decimal> balances = new List<decimal>();
            foreach (Category category in budget.Categories)
            {
                balances.Add(category.Balance);
            }
            return Json(balances);
        }
        
        public ActionResult ShareBudget(string[] userNames, int budgetId)
        {
            foreach (var userName in userNames)
            {
                db.Users.FirstOrDefault(user => user.UserName == userName).Budgets.Add(db.Budgets.Find(budgetId));
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public static void EmailNotification(ApplicationUser user, Transaction transaction, Category category)
        {
            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage();

            // Add the message properties.
            myMessage.From = new MailAddress("aaron.hudson18@gmail.com");

            string reciepient = user.UserName.ToString();

            myMessage.AddTo(reciepient);

            myMessage.Subject = String.Format("{0} is over its limit.", category.ToString());
            myMessage.Text = String.Format("{0} has either reached, or surpassed its limit of {1}. It's current balance is {2}./n/nMost recent transaction: {3}/nDescription: {4}/nDate: {5}/nAmount: {6}", category.ToString(), category.Limit.ToString(), category.Balance.ToString(), transaction.Title, transaction.Description, transaction.CreatedOn, transaction.Amount);

            var credentials = new NetworkCredential("azure_6b7a841809d423ac42c8d26632cdbbb9@azure.com", "IkorTq5Kr7q7M4B");
            var transportWeb = new SendGrid.Web(credentials);
            transportWeb.DeliverAsync(myMessage);
        }
    }
}