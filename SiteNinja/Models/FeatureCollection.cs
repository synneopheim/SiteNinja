using System.ComponentModel.DataAnnotations;

namespace SiteNinja.Models
{
    public class FeatureCollection
    {
        public FeatureCollection(List<Feature> features)
        {
            Features = features;
        }

        public GeometryType Type => GeometryType.FeatureCollection;

        [Required]
        public List<Feature> Features { get; set; }
    }
}
