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
            builder.HasPostgresExtension("uuid-ossp");
            builder.Entity<Account>()
                .HasIndex(a => a.Email)
                .IsUnique();
            builder.Entity<Account>()
                .Property(a => a.AccId)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")   
                .IsRequired();
            builder.Entity<Album>()
                .Property(a => a.AlbumId)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")    
                .IsRequired();
            builder.Entity<Location>()
                .Property(l => l.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")    
                .IsRequired();
            builder.Entity<Photo>()
                .Property(a => a.PhotoId)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")   
                .IsRequired();
            builder.Entity<Race>()
                .Property(r => r.RaceId)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")   
                .IsRequired();
            builder.Entity<RaceAttendance>()
                .Property(a => a.RaceId)
                .HasColumnType("uuid")    
                .IsRequired();
            builder.Entity<RaceRegistration>()
                .Property(a => a.RaceId)
                .HasColumnType("uuid")    
                .IsRequired();
            builder.Entity<RaceRegistration>()
                .Property(a => a.AccId)
                .HasColumnType("uuid")    
                .IsRequired();
            builder.Entity<ForgotPasswordHistory>()
                .HasKey(nameof(ForgotPasswordHistory.Date), nameof(ForgotPasswordHistory.Email));
            builder.Entity<RaceAttendance>()
                .HasKey(nameof(RaceAttendance.RaceId), nameof(RaceAttendance.BibNumber));
            builder.Entity<RaceRegistration>()
                .HasKey(nameof(RaceRegistration.RaceId), nameof(RaceRegistration.AccId), nameof(RaceRegistration.RegistedAt));
            builder.Entity<CheckPoint>()
                .HasKey(nameof(CheckPoint.RFID), nameof(CheckPoint.RaceId), nameof(CheckPoint.ScannerId));
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<ForgotPasswordHistory> ForgotPasswordHistories{ get; set; }
        public DbSet<RaceRegistration> RaceRegistrations{get; set;}

        public DbSet<Album> Albums { get; set; }
        public DbSet<Photo> Photo { get; set; }

        public DbSet<Race> Races { get; set; }

        public DbSet<RaceAttendance> RaceAttendances{ get; set; }
        public DbSet<Location> Locations {get; set;}
        public DbSet<CheckPoint> CheckPoints {get; set;}
    }
}