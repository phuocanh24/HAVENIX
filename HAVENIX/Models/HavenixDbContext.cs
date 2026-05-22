using System.Data.Entity;
using havenix.Models;

namespace havenix.Data
{
    public class HavenixDbContext : DbContext
    {
        public HavenixDbContext() : base("HavenixDbContext")
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}