using System.Threading.Tasks;
using GroceryStore.Core;
using GroceryStore.Core.Contracts;
using GroceryStore.Data;

namespace GroceryStore.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GroceryStoreContext _context;
        private ICustomerRepository _customerRepository;

        public UnitOfWork (GroceryStoreContext context)
        {
            this._context =  context;
            context.Database.EnsureCreated(); //Will ensure database created and seed correctly. https://github.com/dotnet/efcore/issues/11666
        }

        public ICustomerRepository Customer => _customerRepository 
            = _customerRepository ?? new CustomerRepository(_context);
        
        public async Task<int> CommitAsync(){
            return await _context.SaveChangesAsync();
        }
      
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}