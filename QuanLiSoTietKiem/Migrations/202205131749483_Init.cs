namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        FullName = c.String(),
                        Phone = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        IdentityNumber = c.String(),
                        ImagePath = c.String(),
                        Balance = c.Long(nullable: false),
                        Address = c.String(),
                        Sex = c.Int(nullable: false),
                        Email = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SavingBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DepositAmount = c.Long(nullable: false),
                        InterestAmount = c.Long(nullable: false),
                        Type = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        EffectedAt = c.DateTime(nullable: false),
                        ExpirationAt = c.DateTime(nullable: false),
                        ClosingAt = c.DateTime(),
                        CustomerId = c.Int(nullable: false),
                        InterestId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Interests", t => t.InterestId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.InterestId);
            
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Factor = c.Double(nullable: false),
                        EffectedAt = c.DateTime(nullable: false),
                        PeriodID = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Periods", t => t.PeriodID, cascadeDelete: true)
                .Index(t => t.PeriodID);
            
            CreateTable(
                "dbo.Periods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Month = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FormCloses",
                c => new
                    {
                        SavingBookId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        EffectedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.SavingBookId, t.StaffId })
                .ForeignKey("dbo.SavingBooks", t => t.SavingBookId, cascadeDelete: true)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.SavingBookId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Address = c.String(),
                        Sex = c.Int(nullable: false),
                        OfficeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Offices", t => t.OfficeId, cascadeDelete: true)
                .Index(t => t.OfficeId);
            
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ShortName = c.String(),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FormCreates",
                c => new
                    {
                        SavingBookId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        EffectedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
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
            DropForeignKey("dbo.FormCloses", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.Staffs", "OfficeId", "dbo.Offices");
            DropForeignKey("dbo.FormCloses", "SavingBookId", "dbo.SavingBooks");
            DropForeignKey("dbo.SavingBooks", "InterestId", "dbo.Interests");
            DropForeignKey("dbo.Interests", "PeriodID", "dbo.Periods");
            DropForeignKey("dbo.SavingBooks", "CustomerId", "dbo.Customers");
            DropIndex("dbo.FormCreates", new[] { "StaffId" });
            DropIndex("dbo.FormCreates", new[] { "SavingBookId" });
            DropIndex("dbo.Staffs", new[] { "OfficeId" });
            DropIndex("dbo.FormCloses", new[] { "StaffId" });
            DropIndex("dbo.FormCloses", new[] { "SavingBookId" });
            DropIndex("dbo.Interests", new[] { "PeriodID" });
            DropIndex("dbo.SavingBooks", new[] { "InterestId" });
            DropIndex("dbo.SavingBooks", new[] { "CustomerId" });
            DropTable("dbo.FormCreates");
            DropTable("dbo.Offices");
            DropTable("dbo.Staffs");
            DropTable("dbo.FormCloses");
            DropTable("dbo.Periods");
            DropTable("dbo.Interests");
            DropTable("dbo.SavingBooks");
            DropTable("dbo.Customers");
        }
    }
}
