using System;
using System.Threading.Tasks;
using GroceryStore.Core.Models;
using GroceryStore.Services.Contracts;
using GroceryStore.Core.Repositories;
using System.Collections.Generic;
using GroceryStore.Core.Contracts;

namespace GroceryStore.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService (IUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork ;
        }        
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _unitOfWork.Customer.GetAllAsync();
        }
        public async Task<Customer> GetCustomerById(int id)
        {
            return await _unitOfWork.Customer.GetByIdAsync(id);
        }
        public async Task<Customer> AddCustomer(Customer newCustomer)
        {
            await _unitOfWork.Customer.AddAsync(newCustomer);
            await _unitOfWork.CommitAsync();
            return newCustomer;
        }
        public async Task<Customer> UpdateCustomer(int id, Customer newCustomer)
        {
            var customerToUpdate=  await _unitOfWork.Customer.GetByIdAsync(id);
            if (customerToUpdate == null)
            {
                return customerToUpdate; // When Empty, no customer exist with requested Id
            }

            customerToUpdate.Name = newCustomer.Name;            
            await _unitOfWork.CommitAsync();

            return customerToUpdate;
        }
        public async Task DeleteCustomer(Customer customerToBeDeleted)
        {
            _unitOfWork.Customer.Remove(customerToBeDeleted);
            await _unitOfWork.CommitAsync();
        }
    }
}
