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
        public DbSet<SeatEntity> Seats { get; set; }
        public DbSet<SeatTypeEntity> SeatTypes { get; set; }
        public DbSet<AdditionEntity> Additions { get; set; }
        public DbSet<HallAdditionEntity> HallsAdditions { get; set; }
        public DbSet<TicketPriceEntity> TicketsPrices { get; set; }
        public DbSet<ShowtimeAdditionEntity> ShowtimesAdditions { get; set; }
        public DbSet<TicketEntity> Tickets { get; set; }
        public DbSet<TicketAdditionEntity> TicketsAdditions { get; set; }
        public DbSet<TicketSeatEntity> TicketsSeats { get; set; }
        public DbSet<ShowtimeDateEntity> ShowtimesDates { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder
                .Entity<HallAdditionEntity>()
                .HasKey(
                    entity => new
                    {
                        entity.HallId,
                        entity.AdditionId
                    }
                );
            modelBuilder
                .Entity<ShowtimeAdditionEntity>()
                .HasKey(
                    entity => new
                    {
                        entity.ShowtimeId,
                        entity.AdditionId
                    }
                );
            modelBuilder
                .Entity<TicketPriceEntity>()
                .HasKey(
                    entity => new
                    {
                        entity.ShowtimeId,
                        entity.SeatTypeId
                    }
                );
            modelBuilder
                .Entity<TicketAdditionEntity>()
                .HasKey(
                    entity => new
                    {
                        entity.TicketId,
                        entity.AdditionId
                    }
                );
            modelBuilder
                .Entity<TicketSeatEntity>()
                .HasKey(
                    entity => new
                    {
                        entity.TicketId,
                        entity.SeatId
                    }
                );
        }
    }
}