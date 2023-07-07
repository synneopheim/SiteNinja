using Microsoft.AspNetCore.Mvc;
using SiteNinja.Application;
using SiteNinja.Middleware;
using SiteNinja.Models;
using SiteNinja.Storage;
using SiteNinja.Validation;

namespace SiteNinja.Controllers
{
    [ApiController]
    //[Route("building-limits")]
    public class SiteController : ControllerBase
    {
        private readonly ILogger<SiteController> _logger;
        private readonly ISiteDataStore _siteDataStore;

        public SiteController(
            ILogger<SiteController> logger)
        {
            _logger = logger;
            _siteDataStore = new SiteDataStore();
        }

        [HttpPost("building-limits")]
        public Task<IActionResult> PostBuildingLimits(
            [FromBody] SiteModel requestModel)
        {
            return ExceptionHandlingMiddleware.Execute(async () =>
            {
                var buildingLimits = RequestValidator.ValidateAndMapBuildingLimits(requestModel.building_limits);
                var plateaus = RequestValidator.ValidateAndMapPlateaus(requestModel.height_plateaus);

                return await ProcessAndStore(buildingLimits, plateaus);
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

        private async Task<string> ProcessAndStore(List<Polygon> buildingLimits, List<Plateau> plateaus)
        {
            var key = new Guid();
            var splitBuildingLimits = SiteProcessor.FindIntersections(buildingLimits, plateaus);

            var siteDataStorageModel = new SiteDateStorageModel(buildingLimits, plateaus, splitBuildingLimits);

            _siteDataStore.SetData(key.ToString(), siteDataStorageModel, DateTimeOffset.Now.AddDays(1));

            return "Stored processed site data.";
        }
    }

    public record SiteModel(
        FeatureCollection building_limits,
        FeatureCollection height_plateaus);
}
