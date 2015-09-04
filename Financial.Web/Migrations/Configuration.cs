namespace Financial.Web.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

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

            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Password@123");
            context.Users.AddOrUpdate(
                x => x.UserName,
                new ApplicationUser
                {
                    UserName = "aaron@hudson.net",
                    PasswordHash = password,
                    Categories = new Collection<Category>
                    {
                        new Category
                        {
                            Title = "Eating Out",
                            Description = "Available funds to purchase food that is not home-cooked.",
                            Limit = 120m,
                            Transactions = new List<Transaction>
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
                            Transactions = new List<Transaction>
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
                },
                new ApplicationUser
                {
                    UserName = "scott@furgeson.net",
                    PasswordHash = password,
                    Categories = new Collection<Category>
                    {
                        new Category
                        {
                        Title = "Movies",
                        Description = "I LIKE THE MOVIES!",
                        Limit = 45m,
                        Transactions = new List<Transaction>
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
             );

        }
    }
}
