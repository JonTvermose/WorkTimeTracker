namespace WorkTimeTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHolidays : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Days", "Holiday", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Days", "Holiday");
        }
    }
}
