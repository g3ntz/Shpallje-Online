namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedLocationNameMaxlength18 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Posts", new[] { "UserID" });
            CreateIndex("dbo.Users", "UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "UserID" });
            CreateIndex("dbo.Posts", "UserID");
        }
    }
}
