using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using FluentValidation;
using RecommenderApi.Options;

namespace RecommenderApi.Validation
{
    public class UrOptionsValidator : AbstractValidator<UrConfigurationOption>
    {
        public UrOptionsValidator()
        {
            RuleFor(x => x.HarnessUrl)
                .NotEmpty()
                .WithMessage("Harness base URL is required")
                .Must(x =>
                {
                    return Uri.TryCreate(x, UriKind.Absolute, out Uri uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                })
                .WithMessage("Invalid URL format");
        }
    }
}
