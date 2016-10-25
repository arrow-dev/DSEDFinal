namespace DSEDFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrganization : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Organizations", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Organizations", new[] { "OwnerId" });
            AlterColumn("dbo.Organizations", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Organizations", "OwnerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Organizations", "OwnerId");
            AddForeignKey("dbo.Organizations", "OwnerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Organizations", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Organizations", new[] { "OwnerId" });
            AlterColumn("dbo.Organizations", "OwnerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Organizations", "Name", c => c.String());
            CreateIndex("dbo.Organizations", "OwnerId");
            AddForeignKey("dbo.Organizations", "OwnerId", "dbo.AspNetUsers", "Id");
        }
    }
}
