using Cinema.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cinema.DB.EF
{
    public class ApplicationContext: DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ShowtimeEntity> Showtimes { get; set; }
        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<HallEntity> Halls { get; set; }
        public DbSet<CinemaEntity> Cinemas { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}