using Banking.Models;

namespace Banking.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext.DataAccsessContext> 
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Banking.DataAccess.DataContext.DataAccsessContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Accounts.Add(new Account()
            {
                Username = "max",
                Balance = 0,
                Password = "maxmax"
            });

            context.Accounts.Add(new Account()
            {
                Username = "max1",
                Balance = 0,
                Password = "maxmax"
            });
        }
    }
}
