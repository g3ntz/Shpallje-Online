namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedLocationNameMaxlength113 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "UserID", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "UserID" });
            AlterColumn("dbo.Posts", "UserID", c => c.Long(nullable: false));
            CreateIndex("dbo.Posts", "UserID");
            AddForeignKey("dbo.Posts", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "UserID", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "UserID" });
            AlterColumn("dbo.Posts", "UserID", c => c.Long());
            CreateIndex("dbo.Posts", "UserID");
            AddForeignKey("dbo.Posts", "UserID", "dbo.Users", "UserID");
        }
    }
}
