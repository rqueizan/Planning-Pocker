using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Planning.Pocker.Api.NoAuth.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException validationException)
            {
                var msg = validationException.Errors.Select(e => $"{e.PropertyName}, {e.ErrorMessage}");
                var error = new { Validations = msg };
                context.Result = new ObjectResult(error);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                var error = new { Exception = context.Exception.GetType().FullName, context.Exception.Message, context.Exception.StackTrace };
                context.Result = new ObjectResult(error);
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            context.ExceptionHandled = true;
        }
    }
}
