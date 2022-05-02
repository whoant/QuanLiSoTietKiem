namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedAtAndUpdatedAt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Customers", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.SavingBooks", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Interests", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Periods", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Periods", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.FormCloses", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Staffs", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Staffs", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Offices", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Offices", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.FormCreates", "UpdatedAt", c => c.DateTime());
            AlterColumn("dbo.Interests", "CreatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Interests", "CreatedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.FormCreates", "UpdatedAt");
            DropColumn("dbo.Offices", "UpdatedAt");
            DropColumn("dbo.Offices", "CreatedAt");
            DropColumn("dbo.Staffs", "UpdatedAt");
            DropColumn("dbo.Staffs", "CreatedAt");
            DropColumn("dbo.FormCloses", "UpdatedAt");
            DropColumn("dbo.Periods", "UpdatedAt");
            DropColumn("dbo.Periods", "CreatedAt");
            DropColumn("dbo.Interests", "UpdatedAt");
            DropColumn("dbo.SavingBooks", "UpdatedAt");
            DropColumn("dbo.Customers", "UpdatedAt");
            DropColumn("dbo.Customers", "CreatedAt");
        }
    }
}
