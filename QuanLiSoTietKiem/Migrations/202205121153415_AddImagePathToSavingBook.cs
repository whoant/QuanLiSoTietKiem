namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagePathToSavingBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SavingBooks", "ImagePath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SavingBooks", "ImagePath");
        }
    }
}
