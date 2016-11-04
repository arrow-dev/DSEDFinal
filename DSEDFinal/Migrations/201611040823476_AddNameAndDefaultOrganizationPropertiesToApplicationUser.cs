namespace DSEDFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameAndDefaultOrganizationPropertiesToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "DefaultOrganizationId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "DefaultOrganizationId");
            AddForeignKey("dbo.AspNetUsers", "DefaultOrganizationId", "dbo.Organizations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "DefaultOrganizationId", "dbo.Organizations");
            DropIndex("dbo.AspNetUsers", new[] { "DefaultOrganizationId" });
            DropColumn("dbo.AspNetUsers", "DefaultOrganizationId");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
