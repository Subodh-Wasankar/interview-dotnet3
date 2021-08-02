using System;
using System.Threading.Tasks;

namespace GroceryStore.Core.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customer { get; }        
        Task<int> CommitAsync();
    }
}