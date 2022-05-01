namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateFormClose : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormCloses",
                c => new
                    {
                        SavingBookId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SavingBookId, t.StaffId })
                .ForeignKey("dbo.SavingBooks", t => t.SavingBookId, cascadeDelete: true)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.SavingBookId)
                .Index(t => t.StaffId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FormCloses", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.FormCloses", "SavingBookId", "dbo.SavingBooks");
            DropIndex("dbo.FormCloses", new[] { "StaffId" });
            DropIndex("dbo.FormCloses", new[] { "SavingBookId" });
            DropTable("dbo.FormCloses");
        }
    }
}
