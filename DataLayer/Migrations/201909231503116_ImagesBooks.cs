namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagesBooks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ImageData", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ImageData");
        }
    }
}
