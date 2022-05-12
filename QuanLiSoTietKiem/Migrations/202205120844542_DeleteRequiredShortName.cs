namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRequiredShortName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Offices", "ShortName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Offices", "ShortName", c => c.String(nullable: false));
        }
    }
}
