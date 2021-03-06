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

namespace Financial.Web.Controllers
{
    [Authorize]
    public class BudgetsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Home");
            }
            Budget budget = db.Budgets.Find(id);
            BudgetVM budgetVM = new BudgetVM();
            budgetVM.Categories = budget.Categories;
            budgetVM.Description = budget.Description;
            budgetVM.Id = budget.Id;
            budgetVM.Title = budget.Title;
            budgetVM.Users = budget.Users;
            if (budget == null)
            {
                return RedirectToAction("BadRequest", "Home");
            }
            return View(budgetVM);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                if (db.Budgets.Any(b => b.Title == budget.Title))
                {
                    return View(budget);
                }
                    budget.Users = new List<ApplicationUser>()
                {
                    db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId())
                };
                
                db.Budgets.Add(budget);
                db.SaveChanges();
                return RedirectToAction("StartPage", "Home");
            }

            return View(budget);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                if (db.Budgets.Any(b => b.Title == budget.Title))
                {
                    return View(budget);
                }
                db.Entry(budget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("StartPage", "Home");
            }
            return View(budget);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("BadRequest", "Home");
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return RedirectToAction("BadRequest", "Home");
            }
            return View(budget);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Budget budget = db.Budgets.Find(id);
            db.Budgets.Remove(budget);
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
