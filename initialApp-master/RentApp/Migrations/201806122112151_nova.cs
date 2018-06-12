namespace RentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nova : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        AppUserId = c.Int(nullable: false),
                        BranchOfficeStartId = c.Int(nullable: false),
                        BranchOfficeFinishId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        BranchOffice_BranchOfficeId = c.Int(),
                        BranchOffice_BranchOfficeId1 = c.Int(),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: false)
                .ForeignKey("dbo.BranchOffices", t => t.BranchOffice_BranchOfficeId)
                .ForeignKey("dbo.BranchOffices", t => t.BranchOffice_BranchOfficeId1)
                .ForeignKey("dbo.BranchOffices", t => t.BranchOfficeFinishId, cascadeDelete: false)
                .ForeignKey("dbo.BranchOffices", t => t.BranchOfficeStartId, cascadeDelete: false)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.AppUserId)
                .Index(t => t.BranchOfficeStartId)
                .Index(t => t.BranchOfficeFinishId)
                .Index(t => t.VehicleId)
                .Index(t => t.BranchOffice_BranchOfficeId)
                .Index(t => t.BranchOffice_BranchOfficeId1);
            
            CreateTable(
                "dbo.BranchOffices",
                c => new
                    {
                        BranchOfficeId = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        Image = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.BranchOfficeId)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: false)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.PriceLists",
                c => new
                    {
                        PriceListId = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PriceListId)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: false)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.PriceItems",
                c => new
                    {
                        PriceItemId = c.Int(nullable: false, identity: true),
                        PriceListId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        HourPrice = c.Double(nullable: false),
                        Avaliable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PriceItemId)
                .ForeignKey("dbo.PriceLists", t => t.PriceListId, cascadeDelete: false)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: false)
                .Index(t => t.PriceListId)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        Model = c.String(),
                        Maker = c.String(),
                        YearOfMaking = c.DateTime(nullable: false),
                        Image = c.String(),
                        Description = c.String(),
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
                        AppUserId = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        DescriptionScore = c.String(),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: false)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: false)
                .Index(t => t.ServiceId)
                .Index(t => t.AppUserId);
            
            AddColumn("dbo.AppUsers", "Email", c => c.String());
            AddColumn("dbo.AppUsers", "DateBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.AppUsers", "Image", c => c.String());
            AddColumn("dbo.AppUsers", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.AppUsers", "CanCreate", c => c.Boolean(nullable: false));
            AddColumn("dbo.Services", "AppUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Services", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Services", "Logo", c => c.String());
            AddColumn("dbo.Services", "Description", c => c.String());
            AddColumn("dbo.Services", "Email", c => c.String());
            CreateIndex("dbo.Services", "AppUserId");
            AddForeignKey("dbo.Services", "AppUserId", "dbo.AppUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Reservations", "BranchOfficeStartId", "dbo.BranchOffices");
            DropForeignKey("dbo.Reservations", "BranchOfficeFinishId", "dbo.BranchOffices");
            DropForeignKey("dbo.BranchOffices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Reviews", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Reviews", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.PriceLists", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.PriceItems", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.PriceItems", "PriceListId", "dbo.PriceLists");
            DropForeignKey("dbo.Services", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.Reservations", "BranchOffice_BranchOfficeId1", "dbo.BranchOffices");
            DropForeignKey("dbo.Reservations", "BranchOffice_BranchOfficeId", "dbo.BranchOffices");
            DropForeignKey("dbo.Reservations", "AppUserId", "dbo.AppUsers");
            DropIndex("dbo.Reviews", new[] { "AppUserId" });
            DropIndex("dbo.Reviews", new[] { "ServiceId" });
            DropIndex("dbo.Vehicles", new[] { "ServiceId" });
            DropIndex("dbo.PriceItems", new[] { "VehicleId" });
            DropIndex("dbo.PriceItems", new[] { "PriceListId" });
            DropIndex("dbo.PriceLists", new[] { "ServiceId" });
            DropIndex("dbo.Services", new[] { "AppUserId" });
            DropIndex("dbo.BranchOffices", new[] { "ServiceId" });
            DropIndex("dbo.Reservations", new[] { "BranchOffice_BranchOfficeId1" });
            DropIndex("dbo.Reservations", new[] { "BranchOffice_BranchOfficeId" });
            DropIndex("dbo.Reservations", new[] { "VehicleId" });
            DropIndex("dbo.Reservations", new[] { "BranchOfficeFinishId" });
            DropIndex("dbo.Reservations", new[] { "BranchOfficeStartId" });
            DropIndex("dbo.Reservations", new[] { "AppUserId" });
            DropColumn("dbo.Services", "Email");
            DropColumn("dbo.Services", "Description");
            DropColumn("dbo.Services", "Logo");
            DropColumn("dbo.Services", "Approved");
            DropColumn("dbo.Services", "AppUserId");
            DropColumn("dbo.AppUsers", "CanCreate");
            DropColumn("dbo.AppUsers", "Approved");
            DropColumn("dbo.AppUsers", "Image");
            DropColumn("dbo.AppUsers", "DateBirth");
            DropColumn("dbo.AppUsers", "Email");
            DropTable("dbo.Reviews");
            DropTable("dbo.Vehicles");
            DropTable("dbo.PriceItems");
            DropTable("dbo.PriceLists");
            DropTable("dbo.BranchOffices");
            DropTable("dbo.Reservations");
        }
    }
}
