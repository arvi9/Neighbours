using Neighbours.Data;
using Neighbours.Data.Migrations;
using System.Data.Entity;

namespace Neighbours.Web.App_Start
{
    public class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NeighboursDbContext, Configuration>());
            NeighboursDbContext.Create().Database.Initialize(true);
        }
    }
}