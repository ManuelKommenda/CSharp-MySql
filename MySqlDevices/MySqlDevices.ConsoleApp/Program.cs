using MySqlDevices.Core;
using MySqlDevices.Core.Contracts;
using MySqlDevices.Persistence;
using System;
using System.Threading.Tasks;

namespace MySqlDevices.ConsoleApp
{
    class Program
    {
        static async Task Main()
        {
            await InitDataAsync();

            Console.WriteLine("Data imported");
        }

        public static async Task InitDataAsync()
        {
            var usages = await ImportController.ReadFromCsvAsync();

            if (usages.Length == 0)
            {
                Console.WriteLine("No usages found");
                return;
            }

            IUnitOfWork unitOfWork = new UnitOfWork();

            await unitOfWork.DeleteDbAsync();
            await unitOfWork.MigrateDbAsync();

            await unitOfWork.Usages.AddRangeAsync(usages);

            await unitOfWork.SaveChangesAsync();

            Console.WriteLine("Usages saved");
        }
    }
}
