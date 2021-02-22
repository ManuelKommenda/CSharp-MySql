using MySqlDevices.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDevices.Core.Contracts
{
    public interface IUnitOfWork
    {
        IDeviceRepository Devices { get; set; }

        IGenericRepository<Person> People { get; set; }

        IUsageRepository Usages { get; set; }

        Task DeleteDbAsync();

        Task MigrateDbAsync();

        Task<int> SaveChangesAsync();
    }
}
