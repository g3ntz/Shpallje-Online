﻿namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLocationValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locations", "LocationName", c => c.String(nullable: false, maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Locations", "LocationName", c => c.String());
        }
    }
}
