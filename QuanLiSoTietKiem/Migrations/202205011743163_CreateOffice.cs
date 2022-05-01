namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateOffice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ShortName = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Staffs", "OfficeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Staffs", "OfficeId");
            AddForeignKey("dbo.Staffs", "OfficeId", "dbo.Offices", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "OfficeId", "dbo.Offices");
            DropIndex("dbo.Staffs", new[] { "OfficeId" });
            DropColumn("dbo.Staffs", "OfficeId");
            DropTable("dbo.Offices");
        }
    }
}
