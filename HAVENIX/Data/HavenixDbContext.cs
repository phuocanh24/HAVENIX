using System.Data.Entity;
using havenix.Models;

namespace havenix.Data
{
    public class HavenixDbContext : DbContext
    {
        public HavenixDbContext() : base("cinemabooking")
        {
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Showtime> Showtimes { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Genre)
                .IsRequired()
                .HasMaxLength(80);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(x => x.PasswordHash)
                .IsRequired()
                .HasMaxLength(512);

            modelBuilder.Entity<Showtime>()
                .HasRequired(x => x.Movie)
                .WithMany(x => x.Showtimes)
                .HasForeignKey(x => x.MovieId)
                .WillCascadeOnDelete(true);
        }
    }
}