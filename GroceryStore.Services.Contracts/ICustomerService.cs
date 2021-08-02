
using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryStore.Core.Models;

namespace GroceryStore.Services.Contracts
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int id);
        Task<Customer> AddCustomer(Customer newCustomer);
        Task<Customer> UpdateCustomer(int id, Customer newCustomer);
        Task DeleteCustomer(Customer customerToBeDeleted);
       
    }
}