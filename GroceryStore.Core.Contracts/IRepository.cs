using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace GroceryStore.Core.Contracts
{
    // Generic repository contract/interface
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();                
        Task AddAsync(TEntity entity);
       // Task UpdateAsync(TEntity entity);
        void Remove(TEntity entity);
    }
}