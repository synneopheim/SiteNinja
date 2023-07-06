using System.ComponentModel.DataAnnotations;

namespace SiteNinja.Models
{
    public class Feature
    {
        public Feature(Dictionary<string, double> properties, Geometry geometry)
        {
            Properties = properties;
            Geometry = geometry;
        }

        public GeometryType GeometryType => GeometryType.Feature;

        [Required]
        public Dictionary<string, double> Properties { get; set; }

        [Required]
        public Geometry Geometry { get; set; }
    }
}
