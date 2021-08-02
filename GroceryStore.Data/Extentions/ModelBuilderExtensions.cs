using GroceryStore.Data.Configurations;
using GroceryStore.Core.Models;
using Microsoft.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasData(
        new Customer { Id = 1, Name = "Bob" },
        new Customer { Id = 2, Name = "Mary" },
        new Customer { Id = 3, Name = "Joe" }
        );
    }
}
