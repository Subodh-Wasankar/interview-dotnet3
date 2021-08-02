using GroceryStoreAPI.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Test
{
    class GroceryStoreAPIFilterTest
    {
        [Test]
        public async Task Invalid_ModelState_Should_Return_BadRequestObjectResult()
        {
            //Arraneg 
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("", "error");
            var httpContext = new DefaultHttpContext();
            var context = new ActionExecutingContext(
                new ActionContext(
                    httpContext: httpContext,
                    routeData: new RouteData(),
                    actionDescriptor: new ActionDescriptor(),
                    modelState: modelState
                ),
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>().Object);

            var validationAttribute = new ValidateModelAttribute();
            //Act

            validationAttribute.OnActionExecuting(context);

            //Assert
            Assert.IsTrue(context.Result != null
                && context.Result is BadRequestObjectResult);
        }
    }
}
