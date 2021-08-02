using AutoMapper;
using GroceryStore.Core.Models;
using GroceryStore.Services.Contracts;
using GroceryStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{   
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;
    public CustomersController(ICustomerService customerService, IMapper mapper)
    {
        this._customerService = customerService;
        this._mapper = mapper;
    }

    /// <summary>
    /// API to get all customers from the database. 
    /// </summary>
    /// <returns>IEnumerable<CustomerResponse></returns>    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomers();
        var responseCustomers = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResponse>>(customers);
        return Ok(responseCustomers);
    }

    /// <summary>
    /// API to get unique customer matching requested Id from databse, returns null when no match found 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>CustomerResponse</returns>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerResponse>> GetCustomerById(int id)
    {
        if (id == 0)
            return NotFound(Messages.ID_ZERO_ERROR);

        var customer = await _customerService.GetCustomerById(id);

        //Just to demonstrate centralised exception handling, adding this custom argument exception 
        if (customer == null)
            throw new ArgumentException(string.Format(Messages.CUSTOMER_ID_NOT_FOUND, id));

        var responseCustomer = _mapper.Map<Customer, CustomerResponse>(customer);

        return Ok(responseCustomer);
    }

    /// <summary>
    /// API to insert/add customers into the database. If Validation failed, It will return a Bad request response if the customer name is empty or exceeds the Maxlength
    /// </summary>
    /// <param name="newCustomer"></param>
    /// <returns>CustomerResponse</returns>
    [HttpPost]
    public async Task<ActionResult<CustomerResponse>> AddCustomer([FromBody] CustomerRequest newCustomer)
    {
        var newCustomerToInsert = _mapper.Map<CustomerRequest, Customer>(newCustomer);

        var addedCustomer = await _customerService.AddCustomer(newCustomerToInsert);

        var responseCustomer = _mapper.Map<Customer, CustomerResponse>(addedCustomer);

        return Ok(responseCustomer);
    }

    /// <summary>
    /// API to update customer name for requested customer Id. Retunrs bad request if customer Id is not valid or not found. 
    /// If Validation failed, It will return a Bad request response if the customer name is empty or exceeds the Maxlength
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newCustomer"></param>
    /// <returns>CustomerResponse</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerResponse>> UpdateCustomer(int id, [FromBody] CustomerRequest newCustomer)
    {        
        if (id == 0)      
            return NotFound(Messages.ID_ZERO_ERROR);
       
        var customer = _mapper.Map<CustomerRequest, Customer>(newCustomer);

        var updatedCustomer = await _customerService.UpdateCustomer(id, customer);

       //Just to demonstrate centralised exception handling, adding this custom argument exception 
        if (updatedCustomer == null) 
            throw new ArgumentException(string.Format(Messages.CUSTOMER_ID_NOT_FOUND, id));

        var responseCustomer = _mapper.Map<Customer, CustomerResponse>(updatedCustomer);

        return Ok(responseCustomer);
    }
}
