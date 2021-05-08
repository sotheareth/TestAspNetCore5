using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAspNetCore.Filters
{
    public class MySampleActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("MySampleActionFilter OnActionExecuted...");            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("MySampleActionFilter OnActionExecuting...");            
        }

    }
}
