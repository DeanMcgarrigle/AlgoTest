namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedMetricTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MetricData",
                c => new
                    {
                        Team = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Team);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MetricData");
        }
    }
}
