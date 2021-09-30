namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedLocationNameMaxlength19 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "UserID" });
            CreateIndex("dbo.Posts", "UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Posts", new[] { "UserID" });
            CreateIndex("dbo.Users", "UserID");
        }
    }
}
