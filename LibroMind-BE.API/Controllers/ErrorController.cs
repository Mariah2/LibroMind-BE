using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error-development")]
        [ApiExplorerSettings(IgnoreApi = true)]

        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message);
        }

        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (exception?.InnerException is ValidationException validationException && validationException.Errors.Any())
            {
                var modelStateDictionary = new ModelStateDictionary();

                foreach (var error in validationException.Errors)
                {
                    modelStateDictionary.AddModelError(
                        error.PropertyName,
                        error.ErrorMessage);
                }

                return base.ValidationProblem(modelStateDictionary);
            }

            var (statusCode, title) = exception switch
            {
                BadHttpRequestException requestException => (requestException.StatusCode, requestException.Message),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error ocurred")
            };

            return Problem(statusCode: statusCode, title: title);
        }
    }
}
