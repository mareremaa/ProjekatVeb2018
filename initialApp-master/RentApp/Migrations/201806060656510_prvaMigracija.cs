namespace RentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prvaMigracija : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BranchOfficeStartId = c.Int(nullable: false),
                        BranchOfficeFinishId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        BranchOffice_ServiceId = c.Int(),
                        BranchOffice_ServiceId1 = c.Int(),
                        BranchOfficeFinish_ServiceId = c.Int(),
                        BranchOfficeStart_ServiceId = c.Int(),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.BranchOffices", t => t.BranchOffice_ServiceId)
                .ForeignKey("dbo.BranchOffices", t => t.BranchOffice_ServiceId1)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .ForeignKey("dbo.BranchOffices", t => t.BranchOfficeFinish_ServiceId)
                .ForeignKey("dbo.BranchOffices", t => t.BranchOfficeStart_ServiceId)
                .ForeignKey("dbo.AppUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.VehicleId)
                .Index(t => t.BranchOffice_ServiceId)
                .Index(t => t.BranchOffice_ServiceId1)
                .Index(t => t.BranchOfficeFinish_ServiceId)
                .Index(t => t.BranchOfficeStart_ServiceId);
            
            CreateTable(
                "dbo.BranchOffices",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        Service_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ServiceId)
                .ForeignKey("dbo.Services", t => t.Service_Id)
                .Index(t => t.Service_Id);
            
            CreateTable(
                "dbo.PriceLists",
                c => new
                    {
                        PriceListId = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PriceListId)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.PriceItems",
                c => new
                    {
                        PriceItemId = c.Int(nullable: false, identity: true),
                        PriceListId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PriceItemId)
                .ForeignKey("dbo.PriceLists", t => t.PriceListId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.PriceListId)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleId)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: false)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.ServiceId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Services", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Services", "UserId");
            AddForeignKey("dbo.Services", "UserId", "dbo.AppUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.Reservations", "BranchOfficeStart_ServiceId", "dbo.BranchOffices");
            DropForeignKey("dbo.Reservations", "BranchOfficeFinish_ServiceId", "dbo.BranchOffices");
            DropForeignKey("dbo.Services", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.Reviews", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.PriceLists", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Vehicles", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Reservations", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.PriceItems", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.PriceItems", "PriceListId", "dbo.PriceLists");
            DropForeignKey("dbo.BranchOffices", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.Reservations", "BranchOffice_ServiceId1", "dbo.BranchOffices");
            DropForeignKey("dbo.Reservations", "BranchOffice_ServiceId", "dbo.BranchOffices");
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "ServiceId" });
            DropIndex("dbo.Vehicles", new[] { "ServiceId" });
            DropIndex("dbo.PriceItems", new[] { "VehicleId" });
            DropIndex("dbo.PriceItems", new[] { "PriceListId" });
            DropIndex("dbo.PriceLists", new[] { "ServiceId" });
            DropIndex("dbo.Services", new[] { "UserId" });
            DropIndex("dbo.BranchOffices", new[] { "Service_Id" });
            DropIndex("dbo.Reservations", new[] { "BranchOfficeStart_ServiceId" });
            DropIndex("dbo.Reservations", new[] { "BranchOfficeFinish_ServiceId" });
            DropIndex("dbo.Reservations", new[] { "BranchOffice_ServiceId1" });
            DropIndex("dbo.Reservations", new[] { "BranchOffice_ServiceId" });
            DropIndex("dbo.Reservations", new[] { "VehicleId" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropColumn("dbo.Services", "UserId");
            DropTable("dbo.Reviews");
            DropTable("dbo.Vehicles");
            DropTable("dbo.PriceItems");
            DropTable("dbo.PriceLists");
            DropTable("dbo.BranchOffices");
            DropTable("dbo.Reservations");
        }
    }
}
