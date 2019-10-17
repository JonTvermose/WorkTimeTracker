namespace WorkTimeTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDayDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Days", "DateTicks", c => c.Long(nullable: false));
            DropColumn("dbo.Days", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Days", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Days", "DateTicks");
        }
    }
}
