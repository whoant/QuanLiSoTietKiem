namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveImagePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ImagePath", c => c.String(nullable: false));
            DropColumn("dbo.SavingBooks", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SavingBooks", "ImagePath", c => c.String(nullable: false));
            DropColumn("dbo.Customers", "ImagePath");
        }
    }
}
