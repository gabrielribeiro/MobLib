namespace MobLib.Payment.Tests.Migrations
{
    using MobLib.Payment.PayU.Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MobLib.Payment.Tests.TestContext.PayUContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MobLib.Payment.Tests.TestContext.PayUContext context)
        {
            var seeder = new PayUDataSeeder();

            seeder.SeedData(context);
        }
    }
}
