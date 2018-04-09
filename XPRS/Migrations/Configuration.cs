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
                new Contractor { Name = "PeopleTec" }
            );

            context.LaborCategories.AddOrUpdate(lc => lc.PLC, 
                new LaborCategory { PLC = "PA05T", Name = "Program Analyst 5"},
                new LaborCategory { PLC = "PA06T", Name = "Program Analyst 6" },
                new LaborCategory { PLC = "SES01", Name = "Senior Engineer 1" },
                new LaborCategory { PLC = "SES02", Name = "Senior Engineer 2" },
                new LaborCategory { PLC = "PA03T", Name = "Program Analyst 3" },
                new LaborCategory { PLC = "PA07T", Name = "Program Analyst 7" }
            );

            context.SLINs.AddOrUpdate(s => s.Name, 
                new SLIN { Name = "02AD/AE"},
                new SLIN { Name = "02AM"},
                new SLIN { Name = "02AS"},
                new SLIN { Name = "02AV" }

            );

            context.SaveChanges();

            Contractor ctr = context.Contractors.Where(c => c.Name == "Simtech").FirstOrDefault();

            context.ContractorPeriods.AddOrUpdate(c => c.ContractorID,
                new ContractorPeriod { Contractor = ctr, PopStart = DateTime.Parse("5/15/2017"), PopEnd = DateTime.Parse("3/3/2018")},
                new ContractorPeriod { Contractor = ctr, PopStart = DateTime.Parse("3/4/2018"), PopEnd = DateTime.Parse("3/3/2019") }

            );

            context.SaveChanges();

            foreach (ContractorPeriod cp in context.ContractorPeriods.Take(2))
            {
                foreach (LaborCategory lc in context.LaborCategories.Take(4))
                {
                    context.ContractorRates.AddOrUpdate(cr => cr.ContractorRateID, 
                        new ContractorRate { ContractorPeriod = cp, LaborCategory = lc, Type = true, Rate = 100 },
                        new ContractorRate { ContractorPeriod = cp, LaborCategory = lc, Type = false, Rate = 80 }
                    );
                }
            }

            context.SaveChanges();

            context.CostReports.AddOrUpdate(c => c.CostReportID, 
                new CostReport{ Order = context.Orders.FirstOrDefault(), Contractor = "Simtech"}
            );

            context.SaveChanges();

            CostReport cost = context.CostReports.FirstOrDefault();


            foreach (SLIN slin in context.SLINs.Take(4))
            {
                foreach (ContractorRate cr in context.ContractorRates)
                {
                    context.LineItems.AddOrUpdate(ln => ln.LineItemID,
                        new LineItem {
                            SLIN = slin,
                            ContractorRate = cr,
                            CostReport = cost,
                            AllottedHours = 80,
                            BudgetedHours = 80,
                            CurrentHours = 62,
                            CumulativeHours = 62,
                            HoursRemaining = 18,
                            AmountAllotted = 6999.20m,
                            AmountBudgeted = 6999.20m,
                            CurrentAmount = 5424.39m,
                            CumulativeAmount = 5424.39m,
                            AmountRemaining = 1574.81m

                        }
                    );
                }
            }

            context.SaveChanges();
            

        }
    }
}
