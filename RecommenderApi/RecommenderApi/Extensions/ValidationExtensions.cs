using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace RecommenderApi.Extensions
{
    public static class ValidationExtensions
    {
        public static ValidationProblemDetails ToProblemDetails(this ValidationException ex)
        {
            var error = new ValidationProblemDetails()
            {
                Status = 400
            };

            foreach (var err in ex.Errors)
            {
                if (error.Errors.ContainsKey(err.PropertyName))
                {
                    error.Errors[err.PropertyName] = error.Errors[err.PropertyName]
                        .Concat(new[] { err.ErrorMessage }).ToArray();
                    continue;
                }

                error.Errors.Add(new KeyValuePair<string, string[]> (
                    err.PropertyName, new[] { err.ErrorMessage }));
            }

            return error;
        }
    }
}
