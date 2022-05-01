namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRelationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SavingBooks", "DepositAmount", c => c.Long(nullable: false));
            AddColumn("dbo.SavingBooks", "InterestAmount", c => c.Long(nullable: false));
            AddColumn("dbo.SavingBooks", "InterestId", c => c.Int(nullable: false));
            CreateIndex("dbo.SavingBooks", "InterestId");
            AddForeignKey("dbo.SavingBooks", "InterestId", "dbo.Interests", "ID", cascadeDelete: true);
            DropColumn("dbo.SavingBooks", "Deposit");
            DropColumn("dbo.SavingBooks", "Interest");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SavingBooks", "Interest", c => c.Long(nullable: false));
            AddColumn("dbo.SavingBooks", "Deposit", c => c.Long(nullable: false));
            DropForeignKey("dbo.SavingBooks", "InterestId", "dbo.Interests");
            DropIndex("dbo.SavingBooks", new[] { "InterestId" });
            DropColumn("dbo.SavingBooks", "InterestId");
            DropColumn("dbo.SavingBooks", "InterestAmount");
            DropColumn("dbo.SavingBooks", "DepositAmount");
        }
    }
}
