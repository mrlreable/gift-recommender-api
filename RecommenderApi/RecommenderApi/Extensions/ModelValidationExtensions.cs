using FluentValidation.Results;

namespace RecommenderApi.Extensions
{
    public static class ModelValidationExtensions
    {
        public static IDictionary<string, string[]> ToDictionary(this IEnumerable<ValidationFailure> errors)
        {
            return errors
                  .GroupBy(x => x.PropertyName)
                  .ToDictionary(
                      g => g.Key,
                      g => g.Select(x => x.ErrorCode).ToArray()
                  );
        }
    }
}
