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
        public ActionResult BadRequest()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("StartPage");

            return View();
        }

        [AllowAnonymous]
        public ActionResult Menu()
        {
            MenuVM menuVM = new MenuVM();
            if (User.Identity.IsAuthenticated != true)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }
            var rd = ControllerContext.ParentActionViewContext.RouteData;
            var currentAction = rd.GetRequiredString("action");

            if (currentAction != "Details")
            {
                return PartialView("_MenuPartial", menuVM);
            }
            var currentController = rd.GetRequiredString("controller");
            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            menuVM.Budgets = db.Budgets
            .Where(b => b.Users
            .Any(u => u.Id == userId))
            .Select(b => new { Key = b.Title, Value = b.Id })
            .ToDictionary(k => k.Key, v => v.Value);

            if (currentController == "Budgets")
            {
                return PartialView("_MenuPartial", menuVM);
            }

            object parameter;
            rd.Values.TryGetValue("id", out parameter);
            int id;
            Int32.TryParse(parameter.ToString(), out id);

                    menuVM.HasCategories = true;
            menuVM.CurrentBudget = db.Categories.Find(id).BudgetId;

                    var categories = from category in db.Categories
                                 where category.Id == id
                                 join budget in db.Budgets on category.BudgetId equals budget.Id
                                 select budget.Categories;
                    menuVM.Categories = categories.SelectMany(cs => cs
                    .Select(c => new { Key = c.Title, Value = c.Id }))
                    .ToDictionary(k => k.Key, v => v.Value);

                //case "Transactions":
                //    var result = from category in db.Categories
                //                 where category.Id == id
                //                 join budget in db.Budgets on category.BudgetId equals budget.Id
                //                 select budget.Categories;
                //    menuVM.Categories = result.SelectMany(cs => cs
                //    .Select(c => new { Key = c.Title, Value = c.Id }))
                //    .ToDictionary(k => k.Key, v => v.Value);

                //    var transactions = from transaction in db.Transactions
                //                 where transaction.Id == id
                //                 join category in db.Categories on transaction.CategoryId equals category.Id
                //                 select category.Transactions;
                //    menuVM.Transactions = transactions.SelectMany(cs => cs
                //    .Select(c => new { Key = c.Title, Value = c.Id }))
                //    .ToDictionary(k => k.Key, v => v.Value);

                    //menuVM.Transactions = db.Transactions
                    //.Where(c => c.Id == id)
                    //.GroupBy(c => c.Category.Transactions)
                    //.SelectMany(cs => cs
                    //.Select(c => new { Key = c.Title, Value = c.Id }))
                    //.ToDictionary(k => k.Key, v => v.Value);
                    //break;

            return PartialView("_MenuPartial", menuVM);
        }

        public ActionResult StartPage()
        {
            ApplicationUser user = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            List<Budget> budgets = user.Budgets.ToList();
            return View(budgets);
        }

        public ActionResult Graph(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(id);
        }

        public ActionResult CategoryAmounts(int id)
        {
            var categories = db.Budgets.Where(b => b.Id == id)
                .Select(b => b.Categories
                .Select(c => new { Value = c.Transactions
                .Select(t => t.Amount).Sum(), Label = c.Title })).ToList();

            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShareBudget(string userName, int budgetId)
        {
                db.Users.FirstOrDefault(user => user.UserName == userName).Budgets.Add(db.Budgets.Find(budgetId));
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public static void EmailNotification(List<string> users, Transaction transaction, Category category)
        {
            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage();

            // Add the message properties.
            myMessage.From = new MailAddress("aaron.hudson18@gmail.com");

            myMessage.AddTo(users);

            myMessage.Subject = String.Format("{0} is over its limit.", category.Title);
            myMessage.Text = String.Format("{0} has either reached, or surpassed its limit of {1:C}. It's current balance is {2:C}.\r\nMost recent transaction: {3}\r\nDescription: {4}\r\nDate: {5}\r\nAmount: {6}", category.Title, category.Limit, category.Balance, transaction.Title, transaction.Description, transaction.CreatedOn, transaction.FormatedAmount);

            var credentials = new NetworkCredential("aaron.hudson18@gmail.com", "Password@123");
            var transportWeb = new SendGrid.Web(credentials);
            transportWeb.DeliverAsync(myMessage);
        }

        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    base.OnActionExecuted(filterContext);
        //    var viewResult = filterContext.Result as ViewResultBase;
        //    if (viewResult != null)
        //    {
        //        var model = viewResult.ViewData.Model as BaseViewModel;
        //        if (model != null)
        //        {
        //            model.MenuLinks = _repository.GetMenuLinks();
        //        }
        //    }
        }
    }