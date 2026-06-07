namespace HAVENIX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Genre = c.String(nullable: false, maxLength: 80),
                        Duration = c.Int(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        Image = c.String(maxLength: 255),
                        Description = c.String(maxLength: 2000),
                        Language = c.String(maxLength: 50),
                        Status = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Showtimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        Room = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => new { t.MovieId, t.StartTime }, name: "IX_Showtime_Movie_StartTime");
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 255),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Gender = c.String(maxLength: 10),
                        BirthDate = c.DateTime(nullable: false),
                        PasswordHash = c.String(nullable: false, maxLength: 512),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true, name: "IX_User_Email");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Showtimes", "MovieId", "dbo.Movies");
            DropIndex("dbo.Users", "IX_User_Email");
            DropIndex("dbo.Showtimes", "IX_Showtime_Movie_StartTime");
            DropTable("dbo.Users");
            DropTable("dbo.Showtimes");
            DropTable("dbo.Movies");
        }
    }
}
