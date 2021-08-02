using GroceryStore.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace GroceryStoreAPI.Filters
{
    public class ResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ArgumentException argumentException)
            {
                context.Result = new ObjectResult(argumentException.Message)
                {
                    StatusCode = 400  //BAd request, but through Exception to demonstrate exception handeling
                };
                context.ExceptionHandled = true;
            }

            //Any other exception, Response genertic message 
            else if (context.Exception is Exception exception)
            {
                //Log original messasge to Database/File log etc. 

                //Response generic error message 
                context.Result = new ObjectResult(Messages.EXCEPTION_ERROR_MESSAGE)
                {
                    StatusCode = 500  //Server Error
                };
                context.ExceptionHandled = true;
            }
        }
    }

}
