using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using TestAspNetCore_Core.Dtos;
using TestAspNetCore_Core.Dtos.Responses;

namespace TestAspNetCore.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //before go to controller

            if (!context.ModelState.IsValid)
            {
                var errorInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(
                            x => x.ErrorMessage)).ToArray();

                var errorResponse = new ErrorResponseDto();
                foreach(var error in errorInModelState)
                {
                    foreach(var subError in error.Value)
                    {
                        var errorDto = new ErrorDto()
                        {
                            FieldName = error.Key,
                            Message = subError
                        };

                        errorResponse.Errors.Add(errorDto);
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();

            //after go through controller
        }
    }
}
