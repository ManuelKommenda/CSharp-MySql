using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDevices.Core.Contracts
{
    public interface IGenericRepository <T> where T : class
    {
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<T[]> GetAllAsync();
        Task<T> FindByIdAsync(int id);
        Task AddAsnyc(T entity);
        void Update(T entity);
        void Remove(T entity);
        bool Exists(int id);
    }
}
