namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Long(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Infoes",
                c => new
                    {
                        InfoID = c.Long(nullable: false, identity: true),
                        Email = c.String(),
                        PhoneNr = c.String(),
                    })
                .PrimaryKey(t => t.InfoID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationID = c.Long(nullable: false, identity: true),
                        LocationName = c.String(),
                    })
                .PrimaryKey(t => t.LocationID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostID = c.Long(nullable: false, identity: true),
                        LocationID = c.Long(),
                        CategoryID = c.Long(),
                        InfoID = c.Long(),
                        Title = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .ForeignKey("dbo.Infoes", t => t.InfoID)
                .ForeignKey("dbo.Locations", t => t.LocationID)
                .Index(t => t.LocationID)
                .Index(t => t.CategoryID)
                .Index(t => t.InfoID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Long(nullable: false, identity: true),
                        RoleID = c.Long(),
                        InfoID = c.Long(),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Infoes", t => t.InfoID)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .Index(t => t.RoleID)
                .Index(t => t.InfoID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Long(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Users", "InfoID", "dbo.Infoes");
            DropForeignKey("dbo.Posts", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.Posts", "InfoID", "dbo.Infoes");
            DropForeignKey("dbo.Posts", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Users", new[] { "InfoID" });
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.Posts", new[] { "InfoID" });
            DropIndex("dbo.Posts", new[] { "CategoryID" });
            DropIndex("dbo.Posts", new[] { "LocationID" });
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Posts");
            DropTable("dbo.Locations");
            DropTable("dbo.Infoes");
            DropTable("dbo.Categories");
        }
    }
}
