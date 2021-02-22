using MySqlDevices.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MySqlDevices.Core.DataTransferObject
{
    public class UsageDto
    {
        public int Id { get; set; }
        public string DeivceName { get; set; }
        public string DeivceType { get; set; }
        public string SerialNumber { get; set; }
        public string PersonName { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }

        public UsageDto(Usage usage)
        {
            Id = usage.Id;
            DeivceName = usage.Device.Name;
            DeivceType = usage.Device.DeviceType.ToString();
            SerialNumber = usage.Device.SerialNumber;
            PersonName = $"{usage.Person.LastName} {usage.Person.FirstName}";
            From = usage.From;
            To = usage.To;
        }
    }
}
