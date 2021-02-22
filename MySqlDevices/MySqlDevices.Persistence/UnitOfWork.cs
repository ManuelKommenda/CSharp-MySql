using MySqlDevices.Core.Contracts;
using MySqlDevices.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDevices.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IDeviceRepository Devices { get; set; }
        public IGenericRepository<Person> People { get; set; }
        public IUsageRepository Usages { get; set; }

        public UnitOfWork()
        {
            _dbContext = new ApplicationDbContext();
            Devices = new DeviceRepository(_dbContext);
            People = new PersonRepository(_dbContext);
            Usages = new UsageRepository(_dbContext);
        }

        public async Task DeleteDbAsync()
        {
            await _dbContext.Database.EnsureDeletedAsync();
        }

        public async Task MigrateDbAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }
        
        public async Task<int> SaveChangesAsync()
        {
            var entities = _dbContext.ChangeTracker.Entries()
                .Where(entity => entity.State == EntityState.Added
                    || entity.State == EntityState.Modified)
                .Select(e => e.Entity)
                .ToArray();

            return await _dbContext.SaveChangesAsync();
        }
    }
}
