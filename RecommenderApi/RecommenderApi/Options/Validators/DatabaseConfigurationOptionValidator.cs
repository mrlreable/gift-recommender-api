using FluentValidation;
using MongoDB.Driver;

namespace RecommenderApi.Options.Validators
{
    public class DatabaseConfigurationOptionValidator : AbstractValidator<DatabaseConfigurationOption>
    {
        public DatabaseConfigurationOptionValidator()
        {
            RuleFor(x => x.CollectionName)
                .NotEmpty();

            RuleFor(x => x.DatabaseName)
                .NotEmpty();

            RuleFor(x => x.ConnectionString)
                .NotEmpty();
        }
    }
}
