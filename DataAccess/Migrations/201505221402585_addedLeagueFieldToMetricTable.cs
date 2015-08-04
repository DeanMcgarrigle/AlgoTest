namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedLeagueFieldToMetricTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MetricData", "League", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MetricData", "League");
        }
    }
}
