namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropmetricdata : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.MetricData");
        }
    }
}
