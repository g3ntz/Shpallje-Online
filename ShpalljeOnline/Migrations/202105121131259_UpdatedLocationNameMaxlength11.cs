namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedLocationNameMaxlength11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "InfoID", "dbo.Infos");
            DropIndex("dbo.Users", new[] { "InfoID" });
            AlterColumn("dbo.Users", "InfoID", c => c.Long());
            CreateIndex("dbo.Users", "InfoID");
            AddForeignKey("dbo.Users", "InfoID", "dbo.Infos", "InfoID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "InfoID", "dbo.Infos");
            DropIndex("dbo.Users", new[] { "InfoID" });
            AlterColumn("dbo.Users", "InfoID", c => c.Long(nullable: false));
            CreateIndex("dbo.Users", "InfoID");
            AddForeignKey("dbo.Users", "InfoID", "dbo.Infos", "InfoID", cascadeDelete: true);
        }
    }
}
