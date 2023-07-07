using SiteNinja.Models;

namespace SiteNinja.Storage
{
    public class SiteDateStorageModel
    {
        public SiteDateStorageModel(
            List<Polygon> buildingLimits, 
            List<Plateau> plateaus, 
            List<Plateau> splitBuildingLimits)
        {
            BuildingLimits = buildingLimits;
            Plateaus = plateaus;
            SplitBuildingLimits = splitBuildingLimits;
        }

        public List<Polygon> BuildingLimits { get; set; }

        public List<Plateau> Plateaus { get; set; }

        public List<Plateau> SplitBuildingLimits { get; set; }
    }
}
