namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedLocationNameMaxlength16 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "UserID", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "UserID" });
            AddColumn("dbo.Posts", "User_UserID", c => c.Long());
            CreateIndex("dbo.Posts", "User_UserID");
            AddForeignKey("dbo.Posts", "User_UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "User_UserID", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "User_UserID" });
            DropColumn("dbo.Posts", "User_UserID");
            CreateIndex("dbo.Posts", "UserID");
            AddForeignKey("dbo.Posts", "UserID", "dbo.Users", "UserID");
        }
    }
}
