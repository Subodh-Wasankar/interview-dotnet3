using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GroceryStore.Core.Models;
using GroceryStore.Core.Contracts;
using GroceryStore.Data;

namespace GroceryStore.Core.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(GroceryStoreContext context) 
            : base(context)
        { }
        // logic for native repo method for Customer Repository, 
        // impelent ICustomerRepository Contract
    }
}