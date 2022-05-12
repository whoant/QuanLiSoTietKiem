namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredImagePath : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "ImagePath", c => c.String(nullable: false));
        }
    }
}
