namespace Financial.Web.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {

            var store = new UserStore<ApplicationUser>(context);
            var manager = new ApplicationUserManager(store);
            var aaron = new ApplicationUser()
            {
                UserName = "aaron.hudson18@gmail.com",
                Email = "aaron.hudson18@gmail.com"
            };
            manager.Create(aaron, "Password@123");
            var scott = new ApplicationUser()
            {
                UserName = "scott@furgeson.net",
            };
            manager.Create(scott, "Password@123");

            context.Budgets.AddOrUpdate(
                b => b.Title,

                new Budget
                {
                    Title = "Main Budget",
                    Description = "My default budget",
                    Users = new Collection<ApplicationUser> {
                        context.Users.Find("aaron@hudson.net")
                    }
                },
                new Budget
                {
                    Title = "Scotts budget",
                    Description = "My defualt budget",
                    Users = new Collection<ApplicationUser>
                    {
                        context.Users.Find("scott@furgeson.net")
                    }
                }
                );
            context.Categories.AddOrUpdate(
                c => c.BudgetId,

                 new Category
                 {
                     Title = "Eating Out",
                     Description = "Available funds to purchase food that is not home-cooked.",
                     Limit = 120m,
                     BudgetId = context.Budgets.FirstOrDefault(b => b.Title == "Main Budget").Id
                 },
                 new Category
                 {
                     Title = "Shopping",
                     Description = "Available funds to purchase wants and necessities purchaseable at a store.",
                     Limit = 500m,
                     BudgetId = context.Budgets.FirstOrDefault(b => b.Title == "Main Budget").Id
                 },
                  new Category
                  {
                      Title = "Movies",
                      Description = "I LIKE THE MOVIES!",
                      Limit = 45m,
                      BudgetId = context.Budgets.FirstOrDefault(b => b.Title == "Scotts budget").Id
                  }
                );
            context.Transactions.AddOrUpdate(
                t => t.CategoryId,

                 new Transaction
                 {
                     Title = "The Iron Yard",
                     Description = "Saw The Iron Yard",
                     Amount = 8m,
                     CreatedOn = DateTime.Now,
                     CategoryId = context.Categories.FirstOrDefault(c => c.Title == "Movies").Id
                 },
                 new Transaction
                 {
                     Title = "Rubber duckies",
                     Description = "Purchased fun rubber duckies",
                     CreatedOn = DateTime.Now,
                     Amount = 7.32m,
                     CategoryId = context.Categories.FirstOrDefault(c => c.Title == "Shopping").Id
                 },
                  new Transaction
                  {
                      Title = "MgRonalds",
                      Description = "Went to lunch at MgRonalds",
                      CreatedOn = DateTime.Now,
                      Amount = 8.5m,
                      CategoryId = context.Categories.FirstOrDefault(c => c.Title == "Eating out").Id
                  },
                  new Transaction
                  {
                      Title = "Sentucky Fried Chicken",
                      Description = "Took the family to eat SFC",
                      CreatedOn = DateTime.Now,
                      Amount = 32.47m,
                      CategoryId = context.Categories.FirstOrDefault(c => c.Title == "Eating out").Id
                  }
                );

        }
    }
}
