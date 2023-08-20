using FluentValidation;
using RecommenderApi.Dtos;

namespace RecommenderApi.Validation
{
    public class UrInputValidator : AbstractValidator<UrInputDto>
    {
        public UrInputValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User id cannot be empty");
        }
    }
}
