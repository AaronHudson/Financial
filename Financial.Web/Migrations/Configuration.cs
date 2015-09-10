namespace Financial.Web.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Financial.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Financial.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var store = new UserStore<ApplicationUser>(context);
            var manager = new ApplicationUserManager(store);
            var aaron = new ApplicationUser()
            {
                UserName = "aaron@hudson.net",
                Budgets = new Collection<Budget>
                    {
                        new Budget {
                            Title = "Main Budget",
                            Description = "My defualt budget",
                        Categories = new Collection<Category>
                    {
                        new Category
                        {
                            Title = "Eating Out",
                            Description = "Available funds to purchase food that is not home-cooked.",
                            Limit = 120m,
                            Transactions = new Collection<Transaction>
                            {
                                new Transaction
                                {
                                    Title = "MgRonalds",
                                    Description = "Went to lunch at MgRonalds",
                                    CreatedOn = DateTime.Now,
                                    Amount = 8.5m
                                },
                                new Transaction
                                {
                                    Title = "Sentucky Fried Chicken",
                                    Description = "Took the family to eat SFC",
                                    CreatedOn = DateTime.Now,
                                   Amount =  32.47m
                                }
                            }
                        },
                        new Category
                        {
                            Title = "Shopping",
                            Description = "Available funds to purchase wants and necessities purchaseable at a store.",
                            Limit = 500m,
                            Transactions = new Collection<Transaction>
                            {
                                new Transaction
                                {
                                    Title = "Rubber duckies",
                                    Description = "Purchased fun rubber duckies",
                                    CreatedOn = DateTime.Now,
                                    Amount = 7.32m
                                }
                            }
                        }
                    }
                    }
                }
            };
            manager.Create(aaron, "Password@123");
            var scott = new ApplicationUser()
            {
                UserName = "scott@furgeson.net",
                Budgets = new Collection<Budget>
                    {
                        new Budget
                        {
                            Title = "Main Budget",
                            Description = "My defualt budget",
                            Categories = new Collection<Category>
                                {
                                new Category
                                    {
                                    Title = "Movies",
                                    Description = "I LIKE THE MOVIES!",
                                    Limit = 45m,
                                    Transactions = new Collection<Transaction>
                                    {
                                        new Transaction
                                        {
                                            Title = "The Iron Yard",
                                            Description = "Saw The Iron Yard",
                                            Amount = 8m,
                                            CreatedOn = DateTime.Now
                                        }
                                    }
                                    }
                                }
                            }
                    }
            };
            manager.Create(scott, "Password@123");

        }
    }
}
