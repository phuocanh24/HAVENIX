namespace HAVENIX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixMovieImages : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Showtimes");
            Sql("DELETE FROM Movies");

        }

        public override void Down()
        {
        }
    }
}
