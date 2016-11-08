namespace DSEDFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "Job_Id", "dbo.Jobs");
            DropIndex("dbo.Notifications", new[] { "Job_Id" });
            AddColumn("dbo.Notifications", "Hazard_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Notifications", "Hazard_Id");
            AddForeignKey("dbo.Notifications", "Hazard_Id", "dbo.Hazards", "Id", cascadeDelete: true);
            DropColumn("dbo.Notifications", "Job_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "Job_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Notifications", "Hazard_Id", "dbo.Hazards");
            DropIndex("dbo.Notifications", new[] { "Hazard_Id" });
            DropColumn("dbo.Notifications", "Hazard_Id");
            CreateIndex("dbo.Notifications", "Job_Id");
            AddForeignKey("dbo.Notifications", "Job_Id", "dbo.Jobs", "Id", cascadeDelete: true);
        }
    }
}
