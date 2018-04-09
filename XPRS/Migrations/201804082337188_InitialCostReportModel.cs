namespace XPRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCostReportModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractorPeriods",
                c => new
                    {
                        ContractorPeriodID = c.Int(nullable: false, identity: true),
                        ContractorID = c.Int(nullable: false),
                        PopStart = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PopEnd = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ContractorPeriodID)
                .ForeignKey("dbo.Contractors", t => t.ContractorID, cascadeDelete: true)
                .Index(t => t.ContractorID);
            
            CreateTable(
                "dbo.ContractorRates",
                c => new
                    {
                        ContractorRateID = c.Int(nullable: false, identity: true),
                        ContractorPeriodID = c.Int(nullable: false),
                        LaborCategoryID = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Type = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ContractorRateID)
                .ForeignKey("dbo.ContractorPeriods", t => t.ContractorPeriodID, cascadeDelete: true)
                .ForeignKey("dbo.LaborCategories", t => t.LaborCategoryID, cascadeDelete: true)
                .Index(t => t.ContractorPeriodID)
                .Index(t => t.LaborCategoryID);
            
            CreateTable(
                "dbo.LaborCategories",
                c => new
                    {
                        LaborCategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PLC = c.String(),
                    })
                .PrimaryKey(t => t.LaborCategoryID);
            
            CreateTable(
                "dbo.CostReports",
                c => new
                    {
                        CostReportID = c.Int(nullable: false, identity: true),
                        Contractor = c.String(),
                        OrderID = c.Int(nullable: false),
                        ReportingPeriodStart = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ReportingPeriodEnd = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.CostReportID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.LineItems",
                c => new
                    {
                        LineItemID = c.Int(nullable: false, identity: true),
                        CostReportID = c.Int(nullable: false),
                        SLINID = c.Int(nullable: false),
                        ContractorRateID = c.Int(nullable: false),
                        AllottedHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BudgetedHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CumulativeHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HoursRemaining = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountAllotted = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountBudgeted = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CumulativeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountRemaining = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.LineItemID)
                .ForeignKey("dbo.ContractorRates", t => t.ContractorRateID, cascadeDelete: true)
                .ForeignKey("dbo.CostReports", t => t.CostReportID, cascadeDelete: true)
                .ForeignKey("dbo.SLINs", t => t.SLINID, cascadeDelete: true)
                .Index(t => t.CostReportID)
                .Index(t => t.SLINID)
                .Index(t => t.ContractorRateID);
            
            CreateTable(
                "dbo.SLINs",
                c => new
                    {
                        SLINID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SLINID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CostReports", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.LineItems", "SLINID", "dbo.SLINs");
            DropForeignKey("dbo.LineItems", "CostReportID", "dbo.CostReports");
            DropForeignKey("dbo.LineItems", "ContractorRateID", "dbo.ContractorRates");
            DropForeignKey("dbo.ContractorRates", "LaborCategoryID", "dbo.LaborCategories");
            DropForeignKey("dbo.ContractorRates", "ContractorPeriodID", "dbo.ContractorPeriods");
            DropForeignKey("dbo.ContractorPeriods", "ContractorID", "dbo.Contractors");
            DropIndex("dbo.LineItems", new[] { "ContractorRateID" });
            DropIndex("dbo.LineItems", new[] { "SLINID" });
            DropIndex("dbo.LineItems", new[] { "CostReportID" });
            DropIndex("dbo.CostReports", new[] { "OrderID" });
            DropIndex("dbo.ContractorRates", new[] { "LaborCategoryID" });
            DropIndex("dbo.ContractorRates", new[] { "ContractorPeriodID" });
            DropIndex("dbo.ContractorPeriods", new[] { "ContractorID" });
            DropTable("dbo.SLINs");
            DropTable("dbo.LineItems");
            DropTable("dbo.CostReports");
            DropTable("dbo.LaborCategories");
            DropTable("dbo.ContractorRates");
            DropTable("dbo.ContractorPeriods");
        }
    }
}
