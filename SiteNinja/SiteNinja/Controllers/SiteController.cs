using Microsoft.AspNetCore.Mvc;
using SiteNinja.Models;

namespace SiteNinja.Controllers
{
    [ApiController]
    //[Route("building-limits")]
    public class SiteController : ControllerBase
    {
        private readonly ILogger<SiteController> _logger;

        public SiteController(ILogger<SiteController> logger)
        {
            _logger = logger;
        }

        [HttpPost("building-limits")]
        public string PostBuildingLimits(
            [FromBody] SiteModel requestModel)
        {
            return requestModel.building_limits.GetType().Name;
        }

        [HttpPut("building-limits")]
        public string UpdateBuildingLimits()
        {
            return "Updated some building limits.";
        }

        [HttpGet("building-limits")]
        public string GetBuildingLimits()
        {
            return "Got some building limits.";
        }
    }

    public record SiteModel(
        FeatureCollection building_limits,
        FeatureCollection height_plateaus);
}
