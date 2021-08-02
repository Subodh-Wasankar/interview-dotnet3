using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GroceryStore.Core.Contracts;
using GroceryStore.Data;

namespace GroceryStore.Core.Repositories
{
    //Generic repository implementaion 
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly GroceryStoreContext _context;
        
        public Repository(GroceryStoreContext context)
        {
            this._context = context;
        }        
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }     
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }    
        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}