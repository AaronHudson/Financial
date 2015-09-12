using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Financial.Web.Models;
using Microsoft.AspNet.Identity;

namespace Financial.Web.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Create(int categoryId)
        {
            TransactionVM transaction = new TransactionVM();
            transaction.CategoryId = categoryId;
            return View(transaction);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Amount,Title,Description")] TransactionVM transactionVM)
        {
            if (ModelState.IsValid)
            {
                Category category = db.Categories.Find(transactionVM.CategoryId);

                Transaction transaction = new Transaction();
                transaction.Amount = transactionVM.Amount;
                transaction.CreatedOn = DateTime.Now;
                transaction.Description = transactionVM.Description;
                transaction.Title = transactionVM.Title;
                transaction.Category = category;

                db.Transactions.Add(transaction);
                db.SaveChanges();

                // Checking to see if an email must be sent off
                if (category.Balance > category.Limit)
                {
                    List<string> users = new List<string>();
                    users.AddRange(db.Budgets.Find(category.Budget.Id).Users.Select(user => user.UserName).ToList());
                    HomeController.EmailNotification(users, transaction, category);
                }

                return RedirectToAction("StartPage", "Home");
            }

            return View(transactionVM);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Amount,Title,Description")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();

                // Checking to see if an email must be sent off
                Category category = db.Categories.Find(transaction.Category.Id);
                var something = db.Categories.Where(c => c.Id == transaction.Category.Id)
                    .Select(g => new
                    {
                        Balance = g.Balance,
                        Limit = g.Limit,
                        BudgetId = g.Budget.Id
                    });
                if (category.Balance > category.Limit)
                {

                    List<string> users = new List<string>();
                    users.AddRange(db.Budgets.Find(category.Budget.Id).Users.Select(user => user.UserName).ToList());
                    HomeController.EmailNotification(users, transaction, category);
                }

                return RedirectToAction("StartPage", "Home");
            }
            return View(transaction);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("StartPage", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
