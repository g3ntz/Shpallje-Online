namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TranslatedValidationsToAlbanian : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Roles", "RoleName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Roles", "RoleName", c => c.String());
        }
    }
}
