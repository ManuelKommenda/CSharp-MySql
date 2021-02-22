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
            
        }
    }
}
