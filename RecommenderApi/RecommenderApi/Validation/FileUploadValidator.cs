using FluentValidation;
using FluentValidation.Results;
using RecommenderApi.Dtos;

namespace RecommenderApi.Validation
{
    public class FileUploadValidator : AbstractValidator<FielUploadDto>
    {
        public FileUploadValidator()
        {
            RuleFor(x => x.File)
                .NotEmpty()
                .Must(x =>
                {
                    return x.Length != 0;
                })
                .WithMessage("File cannot be empty")
                .Must(x =>
                {
                    return Path.GetExtension(x.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase);
                })
                .WithMessage("File should be CSV");

        }
    }
}
