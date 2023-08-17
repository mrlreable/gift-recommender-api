namespace RecommenderApi.Middlewares
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ValidationExceptionMiddleware(RequestDelegate next, ILogger<ValidationExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                Guid errorId = Guid.NewGuid();

                // Log the exception
                _logger.LogError($"Error on the service side - Error id: {errorId} - Message: {ex}");

                // Call response handling
            }
        }
    }
}
