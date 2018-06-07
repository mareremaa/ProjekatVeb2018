namespace RentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drugaMigracija : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.AppUsers", "Email", c => c.String());
            AddColumn("dbo.AppUsers", "DateBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.AppUsers", "Image", c => c.String());
            AddColumn("dbo.AppUsers", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.AppUsers", "CanCreate", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reservations", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reservations", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BranchOffices", "Image", c => c.String());
            AddColumn("dbo.BranchOffices", "Address", c => c.String());
            AddColumn("dbo.Services", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Services", "Logo", c => c.String());
            AddColumn("dbo.Services", "Description", c => c.String());
            AddColumn("dbo.Services", "Email", c => c.String());
            AddColumn("dbo.PriceItems", "HourPrice", c => c.Double(nullable: false));
            AddColumn("dbo.PriceItems", "Avaliable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Vehicles", "Model", c => c.String());
            AddColumn("dbo.Vehicles", "Maker", c => c.String());
            AddColumn("dbo.Vehicles", "YearOfMaking", c => c.DateTime(nullable: false));
            AddColumn("dbo.Vehicles", "Image", c => c.String());
            AddColumn("dbo.Vehicles", "Description", c => c.String());
            AddColumn("dbo.Reviews", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.Reviews", "DescriptionScore", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "DescriptionScore");
            DropColumn("dbo.Reviews", "Score");
            DropColumn("dbo.Vehicles", "Description");
            DropColumn("dbo.Vehicles", "Image");
            DropColumn("dbo.Vehicles", "YearOfMaking");
            DropColumn("dbo.Vehicles", "Maker");
            DropColumn("dbo.Vehicles", "Model");
            DropColumn("dbo.PriceItems", "Avaliable");
            DropColumn("dbo.PriceItems", "HourPrice");
            DropColumn("dbo.Services", "Email");
            DropColumn("dbo.Services", "Description");
            DropColumn("dbo.Services", "Logo");
            DropColumn("dbo.Services", "Approved");
            DropColumn("dbo.BranchOffices", "Address");
            DropColumn("dbo.BranchOffices", "Image");
            DropColumn("dbo.Reservations", "EndDate");
            DropColumn("dbo.Reservations", "StartDate");
            DropColumn("dbo.AppUsers", "CanCreate");
            DropColumn("dbo.AppUsers", "Approved");
            DropColumn("dbo.AppUsers", "Image");
            DropColumn("dbo.AppUsers", "DateBirth");
            DropColumn("dbo.AppUsers", "Email");
            DropColumn("dbo.AppUsers", "Type");
        }
    }
}
