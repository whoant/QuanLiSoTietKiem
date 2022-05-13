namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "ImageUrl", c => c.String());
        }
    }
}
