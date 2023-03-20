using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Account>()
                .HasIndex(a => a.Email)
                .IsUnique();
            builder.Entity<ForgotPasswordHistory>()
                .HasKey(nameof(ForgotPasswordHistory.Date), nameof(ForgotPasswordHistory.Email));
            builder.Entity<RaceAttendance>()
                .HasKey(nameof(RaceAttendance.RaceId), nameof(RaceAttendance.BibNumber));
            builder.Entity<RaceRegistration>()
                .HasKey(nameof(RaceRegistration.RaceId), nameof(RaceRegistration.AccId), nameof(RaceRegistration.RegistedAt));
            
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<ForgotPasswordHistory> ForgotPasswordHistories{ get; set; }
        public DbSet<RaceRegistration> RaceRegistrations{get; set;}

        public DbSet<Album> Albums { get; set; }
        public DbSet<Photo> Photo { get; set; }

        public DbSet<Race> Races { get; set; }
    }
}