namespace Financial.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Budgets", "Title", c => c.String());
            AddColumn("dbo.Budgets", "Description", c => c.String());
            AddColumn("dbo.Transactions", "Title", c => c.String());
            AddColumn("dbo.Transactions", "Description", c => c.String());
            AlterColumn("dbo.Budgets", "Limit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Transactions", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "Amount", c => c.Int(nullable: false));
            AlterColumn("dbo.Budgets", "Limit", c => c.Int(nullable: false));
            DropColumn("dbo.Transactions", "Description");
            DropColumn("dbo.Transactions", "Title");
            DropColumn("dbo.Budgets", "Description");
            DropColumn("dbo.Budgets", "Title");
        }
    }
}
