using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RecommenderApi.Dtos;
using RecommenderApi.Services;

namespace RecommenderApi.Api.Controllers;

[ApiController]
[Route("/harness")]
public class HarnessController : ControllerBase
{
    private readonly IHarnessService _harnessService;
    private readonly IValidator<UrInputDto> _validator;

    public HarnessController(IHarnessService harnessService,
        IValidator<UrInputDto> validator)
    {
        _harnessService = harnessService ?? throw new ArgumentNullException(nameof(harnessService));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    [HttpPost("input")]
    public async Task<IResult> Input([FromBody] UrInputDto dto)
    {
        await _validator.ValidateAndThrowAsync(dto);

        var result = await _harnessService.GenerateHarnessInputAsync(dto);

        return Results.Ok(result);
    }

    [HttpGet("query/user/{userId}")]
    public async Task<IResult> GetUserBasedRecommendations([FromRoute] string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ValidationException($"Route parameter {nameof(userId)} cannot be empty");
        }

        var result = await _harnessService.UserBasedQueryAsync(userId);

        return Results.Ok(result);
    }

    [HttpGet("query/item")]
    public async Task<IResult> GetItemBasedRecommendations([FromQuery] string[] itemId)
    {
        if (itemId is null)
        {
            throw new ValidationException($"Query parameter {nameof(itemId)} cannot be empty");
        }

        if (itemId.Length == 1)
        {
            var result = await _harnessService.ItemBasedQueryAsync(itemId[0]);
            return Results.Ok(result);
        }

        var itemsResult = await _harnessService.ItemSetBasedQueryAsync(itemId);
        return Results.Ok(itemsResult);
    }
}

public class MultiStatus : IResult
{
    public Task ExecuteAsync(HttpContext httpContext)
    {
        throw new NotImplementedException();
    }
}
