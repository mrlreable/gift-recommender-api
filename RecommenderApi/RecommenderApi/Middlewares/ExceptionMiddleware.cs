using FluentValidation;
using RecommenderApi.Common;
using RecommenderApi.Common.Enums;
using RecommenderApi.Common.Exceptions;
using RecommenderApi.Extensions;
using System.Net;

namespace RecommenderApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        /// <summary>
        /// Method what runs all of Http async Task invoke. 
        /// Wrap the call in a try catch block and call the original call.
        /// In the catch branch, there is a global logging step, and the call for handling the HTTP response returning.
        /// </summary>
        /// <param name="httpContext">Calling context object</param>
        /// <returns>Async Task</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            // Wrap the original call into a try catch block
            try
            {
                // Call the original call.
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                httpContext.Response.StatusCode = 400;
                var error = ex.ToProblemDetails();
                await httpContext.Response.WriteAsJsonAsync(error);
            }
            catch (Exception ex)
            {
                Guid errorId = Guid.NewGuid();

                // Log the exception
                _logger.LogError($"Error on the service side - Error id: {errorId} - Message: {ex}");

                // Call response handling
                await HandleExceptionAsync(httpContext, ex, errorId);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex, Guid errorId)
        {
            HttpStatusCode code;
            string name;
            IDictionary<string, string[]>? validationErrors = null;

            switch (ex)
            {
                case DataNotFoundException:
                    code = HttpStatusCode.NotFound;
                    name = ErrorType.DataNotFound.ToString();
                    validationErrors = (ex as ValidationException)?.Errors?.ToDictionary();
                    break;
                case ValidationException exception:
                    code = HttpStatusCode.BadRequest;
                    name = ErrorType.ModelValidation.ToString();
                    validationErrors = exception?.Errors?.ToDictionary();
                    break;
                case ResourceAlreadyExistsException:
                    code = HttpStatusCode.Conflict;
                    name = ErrorType.Conflict.ToString();
                    break;
                case BadHttpRequestException:
                    code = HttpStatusCode.BadRequest;
                    name = ErrorType.BadRequest.ToString();
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    name = ErrorType.Internal.ToString();
                    validationErrors = new Dictionary<string, string[]>
                    {
                        { "InternalError", new string[2] { ex.Message, ex.InnerException?.Message ?? "" } }
                    };
                    break;
            }

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;
            return httpContext.Response.WriteAsJsonAsync(new ApiErrorDto()
            {
                StatusCode = httpContext.Response.StatusCode,
                Name = name,
                Message = $"{ex.Message} - Error id: {errorId}",
                Errors = validationErrors
            });
        }
    }
}
