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
        public Dictionary<string, double> Properties { get; set; }
        public Geometry Geometry { get; set; }
    }
}
