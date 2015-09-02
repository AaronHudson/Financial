namespace Financial.Web.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;

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
                new FinancialtronUser
                {
                    UserName = "aaron@hudson.net",
                    PasswordHash = password,
                    Budgets = new List<Budget>
                    {
                        new Budget
                        {
                            User = 
                        }
                    }
                },
                new FinancialtronUser
                {
                    UserName = "scott@furgeson.net",
                    PasswordHash = password
                }
             );

        }
    }
}
