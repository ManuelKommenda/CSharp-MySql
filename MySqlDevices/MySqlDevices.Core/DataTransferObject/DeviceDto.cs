using MySqlDevices.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySqlDevices.Core.DataTransferObject
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public string  SerialNumber { get; set; }
        public string Name { get; set; }
        public string DeviceType { get; set; }


        public DeviceDto()
        {

        }

        public DeviceDto(Device device)
        {
            Id = device.Id;
            SerialNumber = device.SerialNumber;
            Name = device.Name;
            DeviceType = device.DeviceType.ToString();
                
        }
    }
}
