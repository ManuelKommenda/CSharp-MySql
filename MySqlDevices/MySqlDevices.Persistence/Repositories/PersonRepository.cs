using MySqlDevices.Core.Contracts;
using MySqlDevices.Core.Entities;
using MySqlDevices.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDevices.Persistence
{
    public class PersonRepository : GenericRepository<Person>
    {
   
        public PersonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
