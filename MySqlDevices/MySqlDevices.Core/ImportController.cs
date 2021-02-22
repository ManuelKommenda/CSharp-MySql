using MySqlDevices.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace MySqlDevices.Core
{
    public class ImportController
    {

        const string Filename = "Usages.csv";

        public static async Task<Usage[]> ReadFromCsvAsync()
        {
            string[][] csvUsage = await MyFile.ReadStringMatrixFromCsvAsync(Filename, true);

            var devices = csvUsage
                .GroupBy(line => line[0])
                .Select(grp => new Device()
                {
                    Name = grp.First()[1],
                    SerialNumber = grp.Key,
                    DeviceType = Enum.Parse<DeviceType>(grp.First()[2])
                }).ToArray();



            var persons = csvUsage
                .GroupBy(line => line[5])
                .Select(grp => new Person()
                {
                    LastName = grp.First()[3],
                    FirstName = grp.First()[4],
                    MailAdress = grp.Key
                }).ToArray();


            return csvUsage
                .Select(line => new Usage()
                {
                    Device = devices.Single(d => d.SerialNumber == line[0]),
                    Person = persons.Single(p => p.MailAdress == line[5]),
                    From = DateTime.Parse(line[6]),
                    To = String.IsNullOrEmpty(line[7]) ? null : (DateTime?)DateTime.Parse(line[7])
                }).ToArray();
        }
    }
}
