namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Offices", "ShortName", c => c.String(nullable: false));
            AlterColumn("dbo.Offices", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Offices", "Name", c => c.String());
            AlterColumn("dbo.Offices", "ShortName", c => c.String());
        }
    }
}
