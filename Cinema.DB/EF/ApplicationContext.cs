using System.IO;
using Cinema.DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Cinema.DB.EF
{
    public class ApplicationContext: DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        private DatabaseOptions _databaseOptions;

        public ApplicationContext(IOptions<DatabaseOptions> options)
        {
            _databaseOptions = options.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databaseOptions.ConnectionString);
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