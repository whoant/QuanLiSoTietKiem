namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateFormCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormCreates",
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
            DropForeignKey("dbo.FormCreates", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.FormCreates", "SavingBookId", "dbo.SavingBooks");
            DropIndex("dbo.FormCreates", new[] { "StaffId" });
            DropIndex("dbo.FormCreates", new[] { "SavingBookId" });
            DropTable("dbo.FormCreates");
        }
    }
}
