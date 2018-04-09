namespace XPRS.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Entities;

    public class ModelContext : DbContext
    {
        // Your context has been configured to use a 'Model' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'XPRS.Models.Model' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model' 
        // connection string in the application configuration file.
        public ModelContext()
            : base("name=Model")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Contractor> Contractors { get; set; }
        public virtual DbSet<Placement> Placements { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<CostReport> CostReports { get; set; }
        public virtual DbSet<LineItem> LineItems { get; set; }
        public virtual DbSet<SLIN> SLINs { get; set; }
        public virtual DbSet<ContractorRate> ContractorRates { get; set; }
        public virtual DbSet<ContractorPeriod> ContractorPeriods { get; set; }
        public virtual DbSet<LaborCategory> LaborCategories { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}