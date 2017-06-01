namespace ExportsOfGoods.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ExportsOfGoods.Models.ExportsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ExportsOfGoods.Models.ExportsContext";
        }

        protected override void Seed(ExportsOfGoods.Models.ExportsContext context)
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
            context.TypeOfInspection.AddOrUpdate(new Models.TypeOfInspecion { Type = "ƒосмотр с выборочным вскрытием", Time = 0.005 },
                new Models.TypeOfInspecion { Type = "ƒосмотр с пересчетом и взвещиванием", Time = 0.01 },
                new Models.TypeOfInspecion { Type = "ƒосмотр со вскрытием всех грузовых мест", Time = 0.02 });
        }
    }
}
