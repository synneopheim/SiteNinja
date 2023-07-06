using Microsoft.AspNetCore.Mvc;
using SiteNinja.Middleware;
using SiteNinja.Models;
using SiteNinja.Validation;

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
        public Task<IActionResult> PostBuildingLimits(
            [FromBody] SiteModel requestModel)
        {
            return ExceptionHandlingMiddleware.Execute(async () =>
            {
                var buildingLimits = RequestValidator.ValidateAndMapBuildingLimits(requestModel.building_limits);
                var plateaus = RequestValidator.ValidateAndMapPlateaus(requestModel.height_plateaus);

                return ProcessAndStore(buildingLimits, plateaus);
            });
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

        private Task ProcessAndStore(List<Polygon> buildingLimits, List<Plateau> plateaus)
        {
            throw new NotImplementedException("You got to process and store, but this " +
                "method does not actually call the processing and storing methods yet. " +
                "But, hey! You got passed the request validation!");
        }
    }

    public record SiteModel(
        FeatureCollection building_limits,
        FeatureCollection height_plateaus);
}
