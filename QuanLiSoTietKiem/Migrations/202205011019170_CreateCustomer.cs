namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCustomer : DbMigration
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
                        Balance = c.Long(nullable: false),
                        Address = c.String(),
                        Sex = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SavingBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Deposit = c.Long(nullable: false),
                        Interest = c.Long(nullable: false),
                        Type = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ExpirationAt = c.DateTime(nullable: false),
                        ClosingAt = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavingBooks", "CustomerId", "dbo.Customers");
            DropIndex("dbo.SavingBooks", new[] { "CustomerId" });
            DropTable("dbo.SavingBooks");
            DropTable("dbo.Customers");
        }
    }
}
