namespace XPRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contractors",
                c => new
                    {
                        ContractorID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ContractorID)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentID = c.Int(nullable: false, identity: true),
                        OriginalFileName = c.String(),
                        UniqueFileName = c.String(maxLength: 20),
                        Contractor_ContractorID = c.Int(),
                        Placement_PlacementID = c.Int(),
                        Order_OrderID = c.Int(),
                    })
                .PrimaryKey(t => t.DocumentID)
                .ForeignKey("dbo.Contractors", t => t.Contractor_ContractorID)
                .ForeignKey("dbo.Placements", t => t.Placement_PlacementID)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .Index(t => t.UniqueFileName, unique: true)
                .Index(t => t.Contractor_ContractorID)
                .Index(t => t.Placement_PlacementID)
                .Index(t => t.Order_OrderID);
            
            CreateTable(
                "dbo.Placements",
                c => new
                    {
                        PlacementID = c.Int(nullable: false, identity: true),
                        ContractorID = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlacementID)
                .ForeignKey("dbo.Contractors", t => t.ContractorID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.ContractorID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        ContractNumber = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.OrderID)
                .Index(t => t.ContractNumber, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Placements", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Documents", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.Documents", "Placement_PlacementID", "dbo.Placements");
            DropForeignKey("dbo.Placements", "ContractorID", "dbo.Contractors");
            DropForeignKey("dbo.Documents", "Contractor_ContractorID", "dbo.Contractors");
            DropIndex("dbo.Orders", new[] { "ContractNumber" });
            DropIndex("dbo.Placements", new[] { "OrderID" });
            DropIndex("dbo.Placements", new[] { "ContractorID" });
            DropIndex("dbo.Documents", new[] { "Order_OrderID" });
            DropIndex("dbo.Documents", new[] { "Placement_PlacementID" });
            DropIndex("dbo.Documents", new[] { "Contractor_ContractorID" });
            DropIndex("dbo.Documents", new[] { "UniqueFileName" });
            DropIndex("dbo.Contractors", new[] { "Name" });
            DropTable("dbo.Orders");
            DropTable("dbo.Placements");
            DropTable("dbo.Documents");
            DropTable("dbo.Contractors");
        }
    }
}
