using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySqlDevices.Core.Entities
{
    public class Usage
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public int DeviceId { get; set; }
        public int PersonId { get; set; }
        public Device Device { get; set; }
        public Person Person { get; set; }
    }
}