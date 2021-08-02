using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Core.Models
{
    public static class Messages
    {
        public const string ID_ZERO_ERROR = "Bad Request- Custonmer Id must not be zero";
        public const string CUSTOMER_ID_NOT_FOUND = "Id : We could not find the Customer with Customer Id:{0}";
        public const string EXCEPTION_ERROR_MESSAGE = "Oops! Something went wrong. Please try again later or contact support!";
    }
}
