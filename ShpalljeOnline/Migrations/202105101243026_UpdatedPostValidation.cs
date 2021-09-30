namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPostValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 55));
            AlterColumn("dbo.Posts", "Description", c => c.String(nullable: false, maxLength: 2500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false));
        }
    }
}
