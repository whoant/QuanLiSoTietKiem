namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedAtToFormCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormCloses", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.FormCreates", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FormCreates", "CreatedAt");
            DropColumn("dbo.FormCloses", "CreatedAt");
        }
    }
}
