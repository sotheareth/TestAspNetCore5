using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAspNetCore.Model;

namespace TestAspNetCore.Validator
{
    public class DeveloperValidator : AbstractValidator<Developer>
    {
        public DeveloperValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty();
        }
    }
}
