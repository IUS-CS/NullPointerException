namespace Chronos.Concrete.Migrations.MigrationB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ConfigurationB : DbMigrationsConfiguration<Chronos.Concrete.ChronosContext>
    {
        public ConfigurationB()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Chronos.Concrete.ChronosContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
