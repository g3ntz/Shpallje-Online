namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedLocationNameMaxlength17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "User_UserID", "dbo.Users");
            DropColumn("dbo.Posts", "UserID");
            RenameColumn(table: "dbo.Posts", name: "User_UserID", newName: "UserID");
            RenameIndex(table: "dbo.Posts", name: "IX_User_UserID", newName: "IX_UserID");
            AddForeignKey("dbo.Posts", "UserID", "dbo.Users", "UserID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "UserID", "dbo.Users");
            RenameIndex(table: "dbo.Posts", name: "IX_UserID", newName: "IX_User_UserID");
            RenameColumn(table: "dbo.Posts", name: "UserID", newName: "User_UserID");
            AddColumn("dbo.Posts", "UserID", c => c.Long());
            AddForeignKey("dbo.Posts", "User_UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
    }
}
