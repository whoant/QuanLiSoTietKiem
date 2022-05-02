namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Interests", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Interests", "CreatedAt", c => c.DateTime());
        }
    }
}
