namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserAndInfoValidation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleID" });
            AlterColumn("dbo.Infos", "Email", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Infos", "PhoneNr", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Users", "RoleID", c => c.Long(nullable: false));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Users", "Surname", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.Users", "RoleID");
            AddForeignKey("dbo.Users", "RoleID", "dbo.Roles", "RoleID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleID" });
            AlterColumn("dbo.Users", "Surname", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Users", "RoleID", c => c.Long());
            AlterColumn("dbo.Infos", "PhoneNr", c => c.String());
            AlterColumn("dbo.Infos", "Email", c => c.String());
            CreateIndex("dbo.Users", "RoleID");
            AddForeignKey("dbo.Users", "RoleID", "dbo.Roles", "RoleID");
        }
    }
}
