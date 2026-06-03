namespace HAVENIX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Status");
        }
    }
}
