namespace Chronos.Models.Migrations.MigrationsA
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ConfigurationA : DbMigrationsConfiguration<Chronos.Models.ApplicationDbContext>
    {
        public ConfigurationA()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Chronos.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
