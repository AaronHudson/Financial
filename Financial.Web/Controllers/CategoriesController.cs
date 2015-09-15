using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Financial.Web.Models;
using System.Globalization;

namespace Financial.Web.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Create(int? categoryId)
        {
            if (categoryId.HasValue)
            {
                CategoryVM vm = new CategoryVM();
                vm.BudgetId = categoryId.Value;
                return View(vm);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BudgetId,Limit,Title,Description")] CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category();
                category.Description = categoryVM.Description;
                category.Limit = Decimal.Parse(categoryVM.Limit, NumberStyles.Currency);
                category.Title = categoryVM.Title;
                category.Budget = db.Budgets.Find(categoryVM.BudgetId);

                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("StartPage", "Home");
            }

            return View(categoryVM);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Limit,Title,Description,BudgetId")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("StartPage", "Home");
            }
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
