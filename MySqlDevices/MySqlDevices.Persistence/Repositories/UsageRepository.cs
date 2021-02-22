using MySqlDevices.Core.Contracts;
using MySqlDevices.Core.Entities;
using MySqlDevices.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDevices.Persistence
{
    public class UsageRepository : GenericRepository<Usage>, IUsageRepository
    {
        public UsageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        override
        public async Task<Usage[]> GetAllAsync()
        {
            return await _dbContext.Usages
                .Include(u => u.Person)
                .Include(u => u.Device)
                .OrderBy(u => u.Id)
                .ToArrayAsync();
        }

        override
        public async Task<Usage> FindByIdAsync(int id)
        {
            return await _dbContext.Usages
                .Include(u => u.Person)
                .Include(u => u.Device)
                .FirstAsync(u => u.Id == id);
        }

        public async Task<Usage[]> GetByDeviceIdAsync(int deviceId)
        {
            return await _dbContext.Usages
                .Include(u => u.Device)
                .Include(u => u.Person)
                .Where(u => u.DeviceId == deviceId)
                .ToArrayAsync();
        }

        public async Task<bool> IsCurrentlyNotAvailableAsync(int id, int deviceId, DateTime from)
        {
            return await _dbContext.Usages
                .Where(u => u.DeviceId == deviceId && u.Id != id && u.To != null)
                .AnyAsync(u => u.To > from && from > u.From);
        }

        public async Task<bool> IsDeviceAlreadyReservedAsync(int id, int deviceId, DateTime to)
        {
            return await _dbContext.Usages
                .Where(u => u.Device.Id == deviceId && u.Id != id)
                .AnyAsync(u => u.From < to && u.To >= to || u.From < to && u.To == null);
        }
    }
}
