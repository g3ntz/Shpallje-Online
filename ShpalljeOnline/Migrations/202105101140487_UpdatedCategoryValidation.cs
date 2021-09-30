﻿namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedCategoryValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false, maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false));
        }
    }
}
