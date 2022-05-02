namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SavingBooks", "EffectedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Interests", "EffectedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.FormCloses", "EffectedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.FormCreates", "EffectedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SavingBooks", "CreatedAt", c => c.DateTime());
            AlterColumn("dbo.Interests", "CreatedAt", c => c.DateTime());
            AlterColumn("dbo.FormCloses", "CreatedAt", c => c.DateTime());
            AlterColumn("dbo.FormCreates", "CreatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FormCreates", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.FormCloses", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Interests", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SavingBooks", "CreatedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.FormCreates", "EffectedAt");
            DropColumn("dbo.FormCloses", "EffectedAt");
            DropColumn("dbo.Interests", "EffectedAt");
            DropColumn("dbo.SavingBooks", "EffectedAt");
        }
    }
}
