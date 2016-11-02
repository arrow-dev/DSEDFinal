namespace DSEDFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobAndHazardToDomainModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hazards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        JobId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsComplete = c.Boolean(nullable: false),
                        Reference = c.String(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hazards", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Hazards", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Jobs", "OrganizationId", "dbo.Organizations");
            DropIndex("dbo.Jobs", new[] { "OrganizationId" });
            DropIndex("dbo.Hazards", new[] { "JobId" });
            DropIndex("dbo.Hazards", new[] { "UserId" });
            DropTable("dbo.Jobs");
            DropTable("dbo.Hazards");
        }
    }
}
