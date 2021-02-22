using MySqlDevices.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDevices.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            return _dbContext.AddRangeAsync(entities);
        }


        public async Task AddAsnyc(T entity)
        {
            await _dbContext.Set<T>()
                .AddAsync(entity);
        }

        public bool Exists(int id)
        {
            return _dbContext.Set<T>().Find(id) != null;
        }

        virtual
        public async Task<T> FindByIdAsync(int id)
        {
            return await _dbContext.Set<T>()
                .FindAsync(id);
        }

        virtual
        public async Task<T[]> GetAllAsync()
        {
            return await _dbContext.Set<T>()
                .ToArrayAsync();
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
