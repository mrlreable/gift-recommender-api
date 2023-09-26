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
                .NotEmpty()
                .Must((option, cstring) =>
                {
                    if (TryCreateConnection(option, cstring))
                    {
                        return true;
                    }
                    return false;
                })
                .WithMessage("Cannot create connection. Check the connection to DB!");
        }

        private bool TryCreateConnection(DatabaseConfigurationOption option, string cstring)
        {
            try
            {
                var client = new MongoClient(cstring);
                var db = client.GetDatabase(option.DatabaseName);
                // try get collection
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
