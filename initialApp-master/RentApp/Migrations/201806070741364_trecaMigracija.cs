namespace RentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trecaMigracija : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AppUsers", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppUsers", "Type", c => c.Int(nullable: false));
        }
    }
}
