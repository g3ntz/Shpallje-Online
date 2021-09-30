namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedLocationNameMaxlength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locations", "LocationName", c => c.String(nullable: false, maxLength: 17));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Locations", "LocationName", c => c.String(nullable: false, maxLength: 25));
        }
    }
}
