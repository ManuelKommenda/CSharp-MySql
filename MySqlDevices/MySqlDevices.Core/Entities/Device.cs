using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MySqlDevices.Core.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public DeviceType DeviceType { get; set; }
        public ICollection<Usage> Usages { get; set; }
        public Device()
        {
            Usages = new List<Usage>();
        }
    }
}
