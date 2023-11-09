using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RecommenderApi.Dtos;
using RecommenderApi.Services;
using RecommenderApi.Validation;

namespace RecommenderApi.Controllers
{
    [ApiController]
    [Route("/harness")]
    public class HarnessController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IHarnessService _harnessService;
        private readonly IValidator<UrInputDto> _validator;

        public HarnessController(IConfiguration configuration, ILogger logger, IHarnessService harnessService, IValidator<UrInputDto> validator)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _harnessService = harnessService ?? throw new ArgumentNullException(nameof(harnessService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [HttpPost("upload")]
        public async Task<IResult> InputFromFile([FromForm] FielUploadDto file)
        {
            throw new NotImplementedException();
        }

        [HttpPost("input")]
        public async Task<IResult> Input([FromBody] UrInputDto dto)
        {
            await _validator.ValidateAndThrowAsync(dto);

            var result = await _harnessService.GenerateHarnessInputAsync(dto);

            return Results.Ok();
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
}
