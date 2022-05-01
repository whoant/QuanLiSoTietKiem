namespace QuanLiSoTietKiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateStaff : DbMigration
    {
        public override void Up()
        {
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
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Staffs");
        }
    }
}
