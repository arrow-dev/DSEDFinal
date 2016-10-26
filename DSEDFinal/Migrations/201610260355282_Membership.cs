namespace DSEDFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Membership : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Memberships",
                c => new
                    {
                        MemberId = c.String(nullable: false, maxLength: 128),
                        OrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MemberId, t.OrganizationId })
                .ForeignKey("dbo.AspNetUsers", t => t.MemberId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.OrganizationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Memberships", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Memberships", "MemberId", "dbo.AspNetUsers");
            DropIndex("dbo.Memberships", new[] { "OrganizationId" });
            DropIndex("dbo.Memberships", new[] { "MemberId" });
            DropTable("dbo.Memberships");
        }
    }
}
