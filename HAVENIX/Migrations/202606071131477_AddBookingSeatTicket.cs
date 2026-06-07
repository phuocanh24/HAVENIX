namespace HAVENIX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookingSeatTicket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ShowtimeId = c.Int(nullable: false),
                        BookingDate = c.DateTime(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Showtimes", t => t.ShowtimeId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ShowtimeId);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeatNumber = c.String(nullable: false, maxLength: 10),
                        IsBooked = c.Boolean(nullable: false),
                        ShowtimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Showtimes", t => t.ShowtimeId, cascadeDelete: true)
                .Index(t => t.ShowtimeId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingId = c.Int(nullable: false),
                        SeatId = c.Int(nullable: false),
                        TicketCode = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .ForeignKey("dbo.Seats", t => t.SeatId)
                .Index(t => t.BookingId)
                .Index(t => t.SeatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tickets", "SeatId", "dbo.Seats");
            DropForeignKey("dbo.Tickets", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "ShowtimeId", "dbo.Showtimes");
            DropForeignKey("dbo.Seats", "ShowtimeId", "dbo.Showtimes");
            DropIndex("dbo.Tickets", new[] { "SeatId" });
            DropIndex("dbo.Tickets", new[] { "BookingId" });
            DropIndex("dbo.Seats", new[] { "ShowtimeId" });
            DropIndex("dbo.Bookings", new[] { "ShowtimeId" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Seats");
            DropTable("dbo.Bookings");
        }
    }
}
