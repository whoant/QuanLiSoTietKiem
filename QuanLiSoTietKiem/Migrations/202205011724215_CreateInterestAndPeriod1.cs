namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateInterestAndPeriod1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Factor = c.Double(nullable: false),
                        createdAt = c.DateTime(nullable: false),
                        PeriodID = c.Int(nullable: false),
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
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Interests", "PeriodID", "dbo.Periods");
            DropIndex("dbo.Interests", new[] { "PeriodID" });
            DropTable("dbo.Periods");
            DropTable("dbo.Interests");
        }
    }
}
