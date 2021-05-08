using FluentValidation;
using TestAspNetCore_Core.Dtos.Requests;

namespace WebApplication1.Validator
{
    public class RegisterCustomerRequestDtoValidator : AbstractValidator<RegisterCustomerRequestDto>
    {
        public RegisterCustomerRequestDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
