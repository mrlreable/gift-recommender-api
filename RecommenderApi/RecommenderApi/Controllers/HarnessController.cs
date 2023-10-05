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
        public async Task<IActionResult> InputFromFile([FromForm] FielUploadDto file)
        {
            throw new NotImplementedException();
        }
    }
}
