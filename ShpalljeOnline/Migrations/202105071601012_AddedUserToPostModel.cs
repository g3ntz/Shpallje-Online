namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserToPostModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "UserID", c => c.Long());
            CreateIndex("dbo.Posts", "UserID");
            AddForeignKey("dbo.Posts", "UserID", "dbo.Users", "UserID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "UserID", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "UserID" });
            DropColumn("dbo.Posts", "UserID");
        }
    }
}
