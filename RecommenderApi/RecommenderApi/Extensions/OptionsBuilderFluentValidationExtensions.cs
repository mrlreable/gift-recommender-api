using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ZstdSharp;

namespace RecommenderApi.Extensions
{
    public static class OptionsBuilderFluentValidationExtensions
    {
        public static OptionsBuilder<TOptions> ValidateFluently<TOptions>(this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
        {
            optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(
                s => new FluentValidationOptions<TOptions>(optionsBuilder.Name, s.GetRequiredService<IValidator<TOptions>>()));
            return optionsBuilder;
        }
    }

    public class FluentValidationOptions<TOptions> : IValidateOptions<TOptions> where TOptions : class
    {
        private readonly IValidator<TOptions> _validator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        public FluentValidationOptions(string? name, IValidator<TOptions> validator)
        {
            _validator = validator;
            Name = name;
        }

        /// <summary>
        /// The options name.
        /// </summary>
        public string? Name { get; }

        /// <summary>
        /// Validates a specific named options instance (or all when <paramref name="name"/> is null).
        /// </summary>
        /// <param name="name">The name of the options instance being validated.</param>
        /// <param name="options">The options instance.</param>
        /// <returns>The <see cref="ValidateOptionsResult"/> result.</returns>
        public ValidateOptionsResult Validate(string? name, TOptions options)
        {
            // Null name is used to configure all named options.
            if (Name != null && Name != name)
            {
                // Ignored if not validating this instance.
                return ValidateOptionsResult.Skip;
            }

            // Ensure options are provided to validate against
            ArgumentNullException.ThrowIfNull(options);

            var validationResult = _validator.Validate(options);

            if (validationResult.IsValid)
            {
                return ValidateOptionsResult.Success;
            }

            var errors = validationResult.Errors.Select(e => $"Options validation failed for {e.PropertyName} with error: {e.ErrorMessage}");
            return ValidateOptionsResult.Fail(errors);
        }
    }
}
