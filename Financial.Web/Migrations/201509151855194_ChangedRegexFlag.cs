namespace Financial.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedRegexFlag : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Budgets", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Transactions", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "Title", c => c.String());
            AlterColumn("dbo.Categories", "Title", c => c.String());
            AlterColumn("dbo.Budgets", "Title", c => c.String());
        }
    }
}
