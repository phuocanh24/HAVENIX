namespace HAVENIX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Showtimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        Room = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Gender = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Movies", "Name", c => c.String());
            AddColumn("dbo.Movies", "ReleaseDate", c => c.String());
            AddColumn("dbo.Movies", "Image", c => c.String());
            AddColumn("dbo.Movies", "Description", c => c.String());
            AddColumn("dbo.Movies", "Language", c => c.String());
            AlterColumn("dbo.Movies", "Duration", c => c.String());
            DropColumn("dbo.Movies", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Title", c => c.String());
            DropForeignKey("dbo.Showtimes", "MovieId", "dbo.Movies");
            DropIndex("dbo.Showtimes", new[] { "MovieId" });
            AlterColumn("dbo.Movies", "Duration", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "Language");
            DropColumn("dbo.Movies", "Description");
            DropColumn("dbo.Movies", "Image");
            DropColumn("dbo.Movies", "ReleaseDate");
            DropColumn("dbo.Movies", "Name");
            DropTable("dbo.Users");
            DropTable("dbo.Showtimes");
        }
    }
}
