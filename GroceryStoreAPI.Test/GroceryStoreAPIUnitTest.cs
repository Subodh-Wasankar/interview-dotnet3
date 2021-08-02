using AutoMapper;
using GroceryStore.Core.Models;
using GroceryStore.Services.Contracts;
using GroceryStoreAPI.Mapper;
using GroceryStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Test
{
    public class GroceryStoreAPIUnitTest
    {
        Mock<ICustomerService> _customerService;
        IMapper _mapper;
        IList<Customer> dummyList = new List<Customer> { new Customer { Id = 1, Name = "Rob" }, new Customer { Id = 2, Name = "Danny" } };
        CustomersController _controller;
        public GroceryStoreAPIUnitTest()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DataTransferMapper());
            });
            _mapper = mockMapper.CreateMapper();
            _customerService = new Mock<ICustomerService>();
            _controller = new CustomersController(_customerService.Object, _mapper);
        }

        [Test]
        public async Task Test_GetAllCustomers_WhenEmptyDatabase_Expect_EmptyListOfDtoObject()
        {
            //Arraneg 
            _customerService.Setup(x => x.GetAllCustomers()).ReturnsAsync(new List<Customer>());

            //Act
            var result = await _controller.GetAllCustomers();
            var okResult = result.Result as OkObjectResult;

            //Assert
            Assert.True(okResult.Value is IEnumerable<CustomerResponse>);
            Assert.True(result.Value == null && okResult.StatusCode == 200);
            Assert.True((okResult.Value as IList<CustomerResponse>).Count == 0);
        }
        [Test]
        public async Task Test_GetAllCustomers_WhenHasCustomers_Expect_CustomerArray()
        {
            //Arraneg            
            _customerService.Setup(x => x.GetAllCustomers()).ReturnsAsync(dummyList);

            //Act
            var result = await _controller.GetAllCustomers();
            var okResult = result.Result as OkObjectResult;
            //Assert

            Assert.IsNotNull(okResult.Value);
            Assert.True(okResult.Value is IEnumerable<CustomerResponse>);
            Assert.True((okResult.Value as IList<CustomerResponse>).Count > 0);
        }
        [Test]
        public async Task Test_GetCustomerById_WhenHasCustomersWithId_Expect_CustomerObjectWithSameCustomer()
        {
            //Arraneg 
            var testCustomerId = 1;
            var dummyCustomer = dummyList.Where(x => x.Id == testCustomerId).FirstOrDefault();

            _customerService.Setup(x => x.GetCustomerById(testCustomerId)).ReturnsAsync(dummyCustomer);

            //Act
            var result = await _controller.GetCustomerById(testCustomerId);
            var okResult = result.Result as OkObjectResult;

            //Assert
            Assert.IsNotNull(okResult.Value);
            Assert.True(okResult.Value is CustomerResponse);
            Assert.True((okResult.Value as CustomerResponse).Id == dummyCustomer.Id && (okResult.Value as CustomerResponse).Name == dummyCustomer.Name);
        }

        [Test]
        public async Task Test_GetCustomerById_WhenIdIsZero_Expect_BadRequestWithZeroIdErrorMessage()
        {
            //Arraneg 
            var testCustomerId = 0;
            var moqCustomer = dummyList.Where(x => x.Id == testCustomerId).FirstOrDefault();

            //Act
            var result = await _controller.GetCustomerById(testCustomerId);
            var notFoundResult = result.Result as NotFoundObjectResult;

            //Assert
            Assert.IsNotNull(notFoundResult.Value == Messages.ID_ZERO_ERROR);
        }

        [Test]
        public async Task Test_GetCustomerById_WhenIdIsNotAvailableInDB_Expect_ArgumentException()
        {
            //Arraneg 
            var testCustomerId = 3; //Only 2 are available in dummyList collection
            var moqCustomer = dummyList.Where(x => x.Id == testCustomerId).FirstOrDefault();

            _customerService.Setup(x => x.GetCustomerById(testCustomerId)).ReturnsAsync(moqCustomer);

            //Act         
            //Assert
            Assert.That(async () =>
            {
                var result = await _controller.GetCustomerById(testCustomerId);
            }, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task Test_AddCustomer_WhenValidCustomer_Expect_CustomerAddedToDatabase()
        {
            //Arraneg 
            var newCustomer = new CustomerRequest { Name = "Bob" };
            var dummyInsertedObject = new Customer { Id = 1, Name = "Bob" };

            _customerService.Setup(x => x.AddCustomer(It.IsAny<Customer>())).ReturnsAsync(dummyInsertedObject);

            //Act
            var result = await _controller.AddCustomer(newCustomer);
            var okResult = result.Result as OkObjectResult;

            //Assert
            Assert.True(okResult.Value is CustomerResponse);
            Assert.True(result.Value == null && okResult.StatusCode == 200);
            Assert.True((okResult.Value as CustomerResponse).Id == dummyInsertedObject.Id 
                && (okResult.Value as CustomerResponse).Name == dummyInsertedObject.Name);

        }
               
        [Test] 
        public async Task Test_UpdateCustomer_WhenValidCustomer_Expect_GetsUpdatedInDatabase()
        {
            //Arraneg 
            var newCustomer = new CustomerRequest { Name = "Maria" };
            var updateToCustomerid = 1;
            var dummyUpdatedObject = new Customer { Id = 1, Name = "Maria" };

            _customerService.Setup(x => x.UpdateCustomer(updateToCustomerid, It.IsAny<Customer>())).ReturnsAsync(dummyUpdatedObject);

            //Act
            var result = await _controller.UpdateCustomer(updateToCustomerid, newCustomer);
            var okResult = result.Result as OkObjectResult;

            //Assert
            Assert.True(okResult.Value is CustomerResponse);
            Assert.True(result.Value == null && okResult.StatusCode == 200);
            Assert.True((okResult.Value as CustomerResponse).Id == dummyUpdatedObject.Id
                && (okResult.Value as CustomerResponse).Name == dummyUpdatedObject.Name);
        }

        public async Task Test_UpdateCustomer_WhenIdisZero_Expect_IDZeroErroMessage()
        {
            //Arraneg 
            var newCustomer = new CustomerRequest { Name = "Maria" };
            var updateToCustomerid = 0;
            var dummyUpdatedObject = new Customer { Id = 1, Name = "Maria" };

            //Act
            var result = await _controller.UpdateCustomer(updateToCustomerid, newCustomer);
            var notFoundResult = result.Result as NotFoundObjectResult;

            //Assert
            Assert.IsNotNull(notFoundResult.Value == Messages.ID_ZERO_ERROR);            
        }
        public async Task Test_UpdateCustomer_WhenIdIsNotAvailableInDB_Expect_ArgumentException()
        {
            //Arraneg 
            var newCustomer = new CustomerRequest { Name = "Maria" };
            var testCustomerId = 99; //Only 2 are available in dummyList collection
            var moqCustomer = dummyList.Where(x => x.Id == testCustomerId).FirstOrDefault();

            _customerService.Setup(x => x.UpdateCustomer(testCustomerId, It.IsAny<Customer>())).ReturnsAsync(moqCustomer);

            //Act
            //Assert
            Assert.That(async () =>
            {
                var result = await _controller.GetCustomerById(testCustomerId);
            }, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        [Obsolete("Covered in Invalid_ModelState_Should_Return_BadRequestObjectResult")]
        public void Test_AddCustomer_WhenCustomerNameBlank_Expect_AbortAndValidationErrorMessage()
        {
            //Arraneg             
            //Act
            //Assert
        }
        [Test]
        [Obsolete("Covered in Invalid_ModelState_Should_Return_BadRequestObjectResult")]
        public void Test_AddCustomer_WhenCustomerNameExceeds50Length_Expect_AbortAndValidationErrorMessage()
        {
            //Arraneg 
            //Act
            //Assert
        }

        [Test]
        [Obsolete("Covered in Invalid_ModelState_Should_Return_BadRequestObjectResult")]
        public void Test_UpdateCustomer_WhenCustomerNameBlank_Expect_AbortAndValidationErrorMessage()
        {
            //Arraneg 
            //Act
            //Assert
        }

        [Test]
        [Obsolete("Covered in Invalid_ModelState_Should_Return_BadRequestObjectResult")]
        public void Test_UpdateCustomer_WhenCustomerNameExceeds50Length_Expect_AbortAndValidationErrorMessage()
        {
            //Arraneg 
            //Act
            //Assert
        }

    }
}