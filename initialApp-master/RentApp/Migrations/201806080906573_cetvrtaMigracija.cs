namespace RentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cetvrtaMigracija : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BranchOffices", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.Reservations", "BranchOffice_ServiceId", "dbo.BranchOffices");
            DropForeignKey("dbo.Reservations", "BranchOffice_ServiceId1", "dbo.BranchOffices");
            DropForeignKey("dbo.Reservations", "BranchOfficeFinish_ServiceId", "dbo.BranchOffices");
            DropForeignKey("dbo.Reservations", "BranchOfficeStart_ServiceId", "dbo.BranchOffices");
            DropIndex("dbo.BranchOffices", new[] { "Service_Id" });
            DropColumn("dbo.BranchOffices", "ServiceId");
            RenameColumn(table: "dbo.Reservations", name: "BranchOfficeFinish_ServiceId", newName: "BranchOfficeFinish_BranchOfficeId");
            RenameColumn(table: "dbo.Reservations", name: "BranchOfficeStart_ServiceId", newName: "BranchOfficeStart_BranchOfficeId");
            RenameColumn(table: "dbo.Reservations", name: "BranchOffice_ServiceId", newName: "BranchOffice_BranchOfficeId");
            RenameColumn(table: "dbo.Reservations", name: "BranchOffice_ServiceId1", newName: "BranchOffice_BranchOfficeId1");
            RenameColumn(table: "dbo.BranchOffices", name: "Service_Id", newName: "ServiceId");
            RenameIndex(table: "dbo.Reservations", name: "IX_BranchOffice_ServiceId", newName: "IX_BranchOffice_BranchOfficeId");
            RenameIndex(table: "dbo.Reservations", name: "IX_BranchOffice_ServiceId1", newName: "IX_BranchOffice_BranchOfficeId1");
            RenameIndex(table: "dbo.Reservations", name: "IX_BranchOfficeFinish_ServiceId", newName: "IX_BranchOfficeFinish_BranchOfficeId");
            RenameIndex(table: "dbo.Reservations", name: "IX_BranchOfficeStart_ServiceId", newName: "IX_BranchOfficeStart_BranchOfficeId");
            DropPrimaryKey("dbo.BranchOffices");
            AddColumn("dbo.BranchOffices", "BranchOfficeId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.BranchOffices", "ServiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.BranchOffices", "ServiceId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.BranchOffices", "BranchOfficeId");
            CreateIndex("dbo.BranchOffices", "ServiceId");
            AddForeignKey("dbo.BranchOffices", "ServiceId", "dbo.Services", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reservations", "BranchOffice_BranchOfficeId", "dbo.BranchOffices", "BranchOfficeId");
            AddForeignKey("dbo.Reservations", "BranchOffice_BranchOfficeId1", "dbo.BranchOffices", "BranchOfficeId");
            AddForeignKey("dbo.Reservations", "BranchOfficeFinish_BranchOfficeId", "dbo.BranchOffices", "BranchOfficeId");
            AddForeignKey("dbo.Reservations", "BranchOfficeStart_BranchOfficeId", "dbo.BranchOffices", "BranchOfficeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "BranchOfficeStart_BranchOfficeId", "dbo.BranchOffices");
            DropForeignKey("dbo.Reservations", "BranchOfficeFinish_BranchOfficeId", "dbo.BranchOffices");
            DropForeignKey("dbo.Reservations", "BranchOffice_BranchOfficeId1", "dbo.BranchOffices");
            DropForeignKey("dbo.Reservations", "BranchOffice_BranchOfficeId", "dbo.BranchOffices");
            DropForeignKey("dbo.BranchOffices", "ServiceId", "dbo.Services");
            DropIndex("dbo.BranchOffices", new[] { "ServiceId" });
            DropPrimaryKey("dbo.BranchOffices");
            AlterColumn("dbo.BranchOffices", "ServiceId", c => c.Int());
            AlterColumn("dbo.BranchOffices", "ServiceId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.BranchOffices", "BranchOfficeId");
            AddPrimaryKey("dbo.BranchOffices", "ServiceId");
            RenameIndex(table: "dbo.Reservations", name: "IX_BranchOfficeStart_BranchOfficeId", newName: "IX_BranchOfficeStart_ServiceId");
            RenameIndex(table: "dbo.Reservations", name: "IX_BranchOfficeFinish_BranchOfficeId", newName: "IX_BranchOfficeFinish_ServiceId");
            RenameIndex(table: "dbo.Reservations", name: "IX_BranchOffice_BranchOfficeId1", newName: "IX_BranchOffice_ServiceId1");
            RenameIndex(table: "dbo.Reservations", name: "IX_BranchOffice_BranchOfficeId", newName: "IX_BranchOffice_ServiceId");
            RenameColumn(table: "dbo.BranchOffices", name: "ServiceId", newName: "Service_Id");
            RenameColumn(table: "dbo.Reservations", name: "BranchOffice_BranchOfficeId1", newName: "BranchOffice_ServiceId1");
            RenameColumn(table: "dbo.Reservations", name: "BranchOffice_BranchOfficeId", newName: "BranchOffice_ServiceId");
            RenameColumn(table: "dbo.Reservations", name: "BranchOfficeStart_BranchOfficeId", newName: "BranchOfficeStart_ServiceId");
            RenameColumn(table: "dbo.Reservations", name: "BranchOfficeFinish_BranchOfficeId", newName: "BranchOfficeFinish_ServiceId");
            AddColumn("dbo.BranchOffices", "ServiceId", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.BranchOffices", "Service_Id");
            AddForeignKey("dbo.Reservations", "BranchOfficeStart_ServiceId", "dbo.BranchOffices", "ServiceId");
            AddForeignKey("dbo.Reservations", "BranchOfficeFinish_ServiceId", "dbo.BranchOffices", "ServiceId");
            AddForeignKey("dbo.Reservations", "BranchOffice_ServiceId1", "dbo.BranchOffices", "ServiceId");
            AddForeignKey("dbo.Reservations", "BranchOffice_ServiceId", "dbo.BranchOffices", "ServiceId");
            AddForeignKey("dbo.BranchOffices", "Service_Id", "dbo.Services", "Id");
        }
    }
}
