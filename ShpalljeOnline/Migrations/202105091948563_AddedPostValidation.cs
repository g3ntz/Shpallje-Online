namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPostValidation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Posts", "LocationID", "dbo.Locations");
            DropIndex("dbo.Posts", new[] { "LocationID" });
            DropIndex("dbo.Posts", new[] { "CategoryID" });
            AlterColumn("dbo.Posts", "LocationID", c => c.Long(nullable: false));
            AlterColumn("dbo.Posts", "CategoryID", c => c.Long(nullable: false));
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Description", c => c.String(nullable: false));
            CreateIndex("dbo.Posts", "LocationID");
            CreateIndex("dbo.Posts", "CategoryID");
            AddForeignKey("dbo.Posts", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
            AddForeignKey("dbo.Posts", "LocationID", "dbo.Locations", "LocationID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.Posts", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Posts", new[] { "CategoryID" });
            DropIndex("dbo.Posts", new[] { "LocationID" });
            AlterColumn("dbo.Posts", "Description", c => c.String());
            AlterColumn("dbo.Posts", "Title", c => c.String());
            AlterColumn("dbo.Posts", "CategoryID", c => c.Long());
            AlterColumn("dbo.Posts", "LocationID", c => c.Long());
            CreateIndex("dbo.Posts", "CategoryID");
            CreateIndex("dbo.Posts", "LocationID");
            AddForeignKey("dbo.Posts", "LocationID", "dbo.Locations", "LocationID");
            AddForeignKey("dbo.Posts", "CategoryID", "dbo.Categories", "CategoryID");
        }
    }
}
