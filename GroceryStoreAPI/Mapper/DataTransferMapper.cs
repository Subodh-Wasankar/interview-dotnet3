using AutoMapper;
using GroceryStoreAPI.Models;
using GroceryStore.Core.Models;

namespace GroceryStoreAPI.Mapper
{
    public class DataTransferMapper : Profile
    {
        public DataTransferMapper()
        {
            // From Core/Domain to DTO object type
            CreateMap<Customer, CustomerResponse>();

            // From DTO to Core/Domain model
            CreateMap<CustomerRequest, Customer>();

            //Other subsequent DTO Mappings 
        }
    }
}
