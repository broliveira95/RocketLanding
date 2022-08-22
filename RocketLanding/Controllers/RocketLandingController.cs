using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RocketLanding.Models;
using RocketLanding.Services;

namespace RocketLanding.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RocketLandingController : ControllerBase
    {

        private readonly ILandingService _landingService;
        private readonly IOptions<LandingOptions> _platformOptions;

        public RocketLandingController(ILandingService landingService, IOptions<LandingOptions> platformOptions)
        {
            _landingService = landingService ?? throw new ArgumentNullException(nameof(landingService));
            _platformOptions = platformOptions ?? throw new ArgumentNullException(nameof(platformOptions));
        }

        [HttpPost]
        public string CheckPlatform(CheckPlatformRequest request)
        {
            return _landingService.CheckPlatform(
                request.RocketId,
                request.PositionX,
                request.PositionY);
        }
    }
}