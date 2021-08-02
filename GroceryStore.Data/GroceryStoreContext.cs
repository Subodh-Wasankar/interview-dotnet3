using GroceryStore.Data.Configurations;
using GroceryStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Data
{
    public class GroceryStoreContext :DbContext 
    {
         public GroceryStoreContext(DbContextOptions<GroceryStoreContext> options)
            : base(options)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new CustomerEntityConfiguration());
           
           //Custom extension method to seed data, provided in datbase.json file 
           builder.Seed();
           base.OnModelCreating(builder);
        }
    }
}
