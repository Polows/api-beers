using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace beersapi.Filters
{
    public class OnlyJsonAttribute : Attribute, IActionFilter
    {
	    public void OnActionExecuting(ActionExecutingContext context)
	    {
		    var request = context.HttpContext.Request;
		    var accept = request.Headers.ContainsKey("Accept") ? request.Headers["Accept"] : StringValues.Empty;
			if (accept == StringValues.Empty || !accept.Any(h => h == "application/json"))
		    {
			  context.Result = new NotFoundResult();  
		    }
	    }

	    public void OnActionExecuted(ActionExecutedContext context)
	    {
	    }
    }
}
