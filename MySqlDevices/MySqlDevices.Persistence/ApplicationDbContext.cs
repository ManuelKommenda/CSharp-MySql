using MySqlDevices.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace MySqlDevices.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Usage> Usages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", true, true);
            var configuration = configurationBuilder.Build();
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];

            //Todo
            optionsBuilder.UseMySQL(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RowVersion).IsConcurrencyToken();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(40);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(40);
                entity.Property(e => e.MailAdress).IsRequired().HasMaxLength(60);
                entity.HasMany(e => e.Usages).WithOne(u => u.Person);
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RowVersion).IsConcurrencyToken();
                entity.Property(e => e.SerialNumber).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(40);
                entity.Property(e => e.DeviceType).IsRequired();
                entity.HasMany(e => e.Usages).WithOne(u => u.Device);
            });

            modelBuilder.Entity<Usage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RowVersion).IsConcurrencyToken();
                entity.Property(e => e.From).IsRequired();
                entity.Property(e => e.To);
                entity.HasOne(e => e.Person).WithMany(u => u.Usages).HasForeignKey(e => e.PersonId);
                entity.HasOne(e => e.Device).WithMany(u => u.Usages).HasForeignKey(e => e.DeviceId);
            });
        }
    }
}
