using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Northwind.Services.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Northwind.Services.Data.NorthwindContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Northwind.Services.Data.NorthwindContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
