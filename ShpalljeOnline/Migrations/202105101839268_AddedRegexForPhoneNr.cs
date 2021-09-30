namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRegexForPhoneNr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Infos", "PhoneNr", c => c.String(nullable: false, maxLength: 11));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Infos", "PhoneNr", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
