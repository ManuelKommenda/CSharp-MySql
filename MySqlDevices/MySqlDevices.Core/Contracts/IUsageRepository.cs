using MySqlDevices.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDevices.Core.Contracts
{
    public interface IUsageRepository : IGenericRepository<Usage>
    {
        Task<Usage[]> GetByDeviceIdAsync(int deviceId);

        Task<bool> IsCurrentlyNotAvailableAsync(int id, int deviceId, DateTime from);

        Task<bool> IsDeviceAlreadyReservedAsync(int id, int deviceId, DateTime to);
    }
}
