namespace XPRS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using XPRS.Models.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<XPRS.Models.ModelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(XPRS.Models.ModelContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            Order vir = new Order { OrderID = 1, ContractNumber = "OS560011084" };
            Order battle = new Order { OrderID = 2, ContractNumber = "OS580011084" };
            context.Orders.AddOrUpdate(o => o.ContractNumber, vir, battle);



            context.Contractors.AddOrUpdate(c => c.Name,
                new Contractor { Name = "Simtech" },
                new Contractor { Name = "SAIC" },
                new Contractor { Name = "Torch" },
                new Contractor { Name = "Quantitech" },
                new Contractor { Name = "PeopleTect" }
            );

        }
    }
}
