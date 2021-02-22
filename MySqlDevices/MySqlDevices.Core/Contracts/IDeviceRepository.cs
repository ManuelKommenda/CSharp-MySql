using MySqlDevices.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDevices.Core.Contracts
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        Task<Device> GetBySerialNumberAsync(string serialNumber);
        Task<Device> GetByNameAsync(string name);
    }
}
