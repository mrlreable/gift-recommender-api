namespace RecommenderApi.Common.Enums
{
    public enum ErrorType
    {
        Internal = 1,
        Authorization,
        BadRequest,
        DataNotFound,
        LogicalValidation,
        ModelValidation,
        Conflict
    }
}