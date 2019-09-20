namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Authors",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FirstName = c.String(maxLength: 100),
            //            LastName = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Books",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            AuthorId = c.Int(),
            //            Title = c.String(nullable: false, maxLength: 150),
            //            Pages = c.Int(),
            //            Price = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Authors", t => t.AuthorId)
            //    .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            //DropIndex("dbo.Books", new[] { "AuthorId" });
            //DropTable("dbo.Books");
            //DropTable("dbo.Authors");
        }
    }
}
