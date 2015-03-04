using System.Data.Entity;
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

        public DbSet<SomeTable> SomeTables { get; set; }
    }
}
