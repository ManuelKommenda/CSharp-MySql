using MySqlDevices.Core.Contracts;
using MySqlDevices.Core.Entities;
using MySqlDevices.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDevices.Persistence
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {

        public DeviceRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
                
        }

        public async Task<Device> GetByNameAsync(string name)
        {

            return await _dbContext.Devices
                .SingleOrDefaultAsync(d => d.Name == name);
        }

        public async Task<Device> GetBySerialNumberAsync(string serialNumber)
        {
            return await _dbContext.Devices
                .SingleOrDefaultAsync(d => d.SerialNumber == serialNumber);
        }
    }
}
