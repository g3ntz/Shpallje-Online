namespace ShpalljeOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedInfoTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Infoes", newName: "Infos");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Infos", newName: "Infoes");
        }
    }
}
