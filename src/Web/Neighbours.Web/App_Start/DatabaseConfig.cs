namespace Neighbours.Web.App_Start
{
    using System.Data.Entity;
    using Neighbours.Data;
    using Neighbours.Data.Migrations;

    public class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NeighboursDbContext, Configuration>());
            NeighboursDbContext.Create().Database.Initialize(true);
        }
    }
}