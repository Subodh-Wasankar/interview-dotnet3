using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Validators
{
    public class CustomerRequestModelValidator : AbstractValidator<CustomerRequest>
    {
        public CustomerRequestModelValidator() => RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(50); //Assumed to be 50. Not mentioned in the requirements, but good to have. 
    }
}
