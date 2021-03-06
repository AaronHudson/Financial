﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Financial.Web.Models;
using Microsoft.AspNet.Identity;
using System.Globalization;
using System.Collections;

namespace Financial.Web.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Create(int? categoryId, int? budgetId)
        {
            if (categoryId.HasValue)
            {
                TransactionVM transaction = new TransactionVM();
                transaction.CategoryId = categoryId.Value;
                var query = from category in db.Categories
                            where category.Id == categoryId
                            join budget in db.Budgets on category.BudgetId equals budget.Id
                            select budget.Categories; ;
                var dictionary = query.SelectMany(cs => cs
                    .Select(c => new { Key = c.Title, Value = c.Id }))
                    .ToDictionary(k => k.Key, v => v.Value);
                transaction.Categories = new SelectList((IEnumerable)dictionary, "Value", "Key");
                return View(transaction);
            }else
            if (budgetId.HasValue)
            {
                TransactionVM transaction = new TransactionVM();
                var budget = budgetId.Value;
                var dictionary = db.Budgets
                    .Where(b => b.Id == budget)
                    .Select(b => b.Categories)
                    .SelectMany(cs => cs
                    .Select(c => new { Key = c.Title, Value = c.Id }))
                    .ToDictionary(k => k.Key, v => v.Value);
                transaction.Categories = new SelectList((IEnumerable)dictionary, "Value", "Key");
                return View(transaction);
            }
            return RedirectToAction("BadRequest", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Amount,Title,Description")] TransactionVM transactionVM)
        {
            if (ModelState.IsValid)
            {
                Transaction transaction = new Transaction();
                transaction.Amount = Decimal.Parse(transactionVM.Amount, NumberStyles.Currency);
                transaction.CreatedOn = DateTime.Now;
                transaction.Description = transactionVM.Description;
                transaction.Title = transactionVM.Title;
                transaction.CategoryId = transactionVM.CategoryId;

                db.Transactions.Add(transaction);
                db.SaveChanges();

                // Checking to see if an email must be sent off
                var categoryInformation = db.Categories
                    .Where(c => c.Id == transaction.CategoryId)
                     .Select(a => new
                     {
                         Amount = a.Transactions.Select(t => t.Amount).Sum(),
                         Limit = a.Limit,
                     }).FirstOrDefault();

                if (categoryInformation.Amount > categoryInformation.Limit)
                {
                    List<string> users = new List<string>();
                    Category category = db.Categories.Find(transaction.CategoryId);
                    users.AddRange(db.Budgets.Find(category.Budget.Id).Users.Select(user => user.UserName).ToList());
                    HomeController.EmailNotification(users, transaction, category);
                }

                return RedirectToAction("Details", "Categories", new { id = transaction.CategoryId });
            }

            return View(transactionVM);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Home");
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return RedirectToAction("BadRequest", "Home");
            }
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Amount,Title,Description,CategoryId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();

                // Checking to see if an email must be sent off
                var categoryInformation = db.Categories.Where(c => c.Id == transaction.CategoryId)
                    .Select(a => new
                    {
                        Amount = a.Transactions.Select(t => t.Amount).Sum(),
                        Limit = a.Limit,
                    }).FirstOrDefault();

                if (categoryInformation.Amount > categoryInformation.Limit)
                {
                    List<string> users = new List<string>();
                    Category category = db.Categories.Find(transaction.CategoryId);
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
                return RedirectToAction("BadRequest", "Home");
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
