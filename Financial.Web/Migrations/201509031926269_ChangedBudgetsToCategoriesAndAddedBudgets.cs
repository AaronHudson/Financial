namespace Financial.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedBudgetsToCategoriesAndAddedBudgets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Limit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Title = c.String(),
                        Description = c.String(),
                        Budget_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Budgets", t => t.Budget_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Budget_Id)
                .Index(t => t.User_Id);
            
            DropColumn("dbo.Budgets", "Limit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Budgets", "Limit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.Categories", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Categories", "Budget_Id", "dbo.Budgets");
            DropIndex("dbo.Categories", new[] { "User_Id" });
            DropIndex("dbo.Categories", new[] { "Budget_Id" });
            DropTable("dbo.Categories");
        }
    }
}
