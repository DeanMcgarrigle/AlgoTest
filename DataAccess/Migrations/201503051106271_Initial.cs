namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LeagueData",
                c => new
                    {
                        DateTime = c.DateTime(nullable: false),
                        HomeTeam = c.String(nullable: false, maxLength: 128),
                        AwayTeam = c.String(nullable: false, maxLength: 128),
                        League = c.String(),
                        FullTimeHomeGoals = c.Int(nullable: false),
                        FullTimeAwayGoals = c.Int(nullable: false),
                        FullTimeResult = c.String(),
                        HalfTimeHomeGoals = c.Int(nullable: false),
                        HalfTimeAwayGoals = c.Int(nullable: false),
                        HalfTimeResult = c.String(),
                        Referee = c.String(),
                        HomeTeamShots = c.Int(nullable: false),
                        AwayTeamShots = c.Int(nullable: false),
                        HomeTeamShotsOnTarget = c.Int(nullable: false),
                        AwayTeamShotsOnTarget = c.Int(nullable: false),
                        HomeTeamFoulsCommited = c.Int(nullable: false),
                        AwayTeamFoulsCommited = c.Int(nullable: false),
                        HomeTeamCorners = c.Int(nullable: false),
                        AwayTeamCorners = c.Int(nullable: false),
                        HomeTeamYellowCards = c.Int(nullable: false),
                        AwayTeamYellowCards = c.Int(nullable: false),
                        HomeTeamRedCards = c.Int(nullable: false),
                        AwayTeamRedCards = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DateTime, t.HomeTeam, t.AwayTeam });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LeagueData");
        }
    }
}
