namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MetricData", "OverallForm", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MetricData", "HomeForm", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MetricData", "AwayForm", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MetricData", "HomeGoalsScored", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MetricData", "AwayGoalsScored", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MetricData", "HomeGoalsConceded", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MetricData", "AwayGoalsConceded", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MetricData", "AvgShotsAttemptedHome", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MetricData", "AvgShotsAttemptedAway", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MetricData", "AvgShotsOnTarget", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MetricData", "AvgCornersHome", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MetricData", "AvgCornersAway", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MetricData", "AvgCornersConcededHome", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MetricData", "AvgCornersConcededAway", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MetricData", "AvgGoalsPerGameHome", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MetricData", "AvgGoalsPerGameAway", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MetricData", "AvgGoalsPerGameAway");
            DropColumn("dbo.MetricData", "AvgGoalsPerGameHome");
            DropColumn("dbo.MetricData", "AvgCornersConcededAway");
            DropColumn("dbo.MetricData", "AvgCornersConcededHome");
            DropColumn("dbo.MetricData", "AvgCornersAway");
            DropColumn("dbo.MetricData", "AvgCornersHome");
            DropColumn("dbo.MetricData", "AvgShotsOnTarget");
            DropColumn("dbo.MetricData", "AvgShotsAttemptedAway");
            DropColumn("dbo.MetricData", "AvgShotsAttemptedHome");
            DropColumn("dbo.MetricData", "AwayGoalsConceded");
            DropColumn("dbo.MetricData", "HomeGoalsConceded");
            DropColumn("dbo.MetricData", "AwayGoalsScored");
            DropColumn("dbo.MetricData", "HomeGoalsScored");
            DropColumn("dbo.MetricData", "AwayForm");
            DropColumn("dbo.MetricData", "HomeForm");
            DropColumn("dbo.MetricData", "OverallForm");
        }
    }
}
