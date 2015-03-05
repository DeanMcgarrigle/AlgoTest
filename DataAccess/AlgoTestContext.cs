using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Model;

namespace DataAccess
{
    public class AlgoTestContext : DbContext
    {
        public AlgoTestContext()
            : base(
                string.Format(
                    @"Server={0};Database={1};User ID={2};Password={3};Trusted_Connection=False;Encrypt=True;Connection Timeout=30;",
                    DbInfo.DbServer, DbInfo.DbName, DbInfo.DbUser, DbInfo.DbPassword))
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(LeagueDataMap).Assembly);
        }

        public DbSet<LeagueData> LeagueData { get; set; }

    }

    public class LeagueDataMap : EntityTypeConfiguration<LeagueData>
    {
        public LeagueDataMap()
        {
            ToTable("LeagueData");
            HasKey(x => new { x.DateTime, x.HomeTeam, x.AwayTeam });
        }
    }
}
