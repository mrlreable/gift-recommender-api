namespace RecommenderApi.Common.Enums
{
    public enum ErrorType
    {
        Internal = 1,
        Authorization,
        DataNotFound,
        LogicalValidation,
        ModelValidation,
        Conflict
    }
}