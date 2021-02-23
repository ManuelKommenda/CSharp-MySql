using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MySqlDevices.Core.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MailAdress { get; set; }
        public string FullName => FirstName + " " + LastName;
        public ICollection<Usage> Usages { get; set; }

        public Person()
        {
            Usages = new List<Usage>();
        }

    }
}
