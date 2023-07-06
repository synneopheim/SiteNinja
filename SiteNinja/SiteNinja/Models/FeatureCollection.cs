namespace SiteNinja.Models
{
    public class FeatureCollection
    {
        public FeatureCollection(List<Feature> features)
        {
            Features = features;
        }

        public GeometryType Type => GeometryType.FeatureCollection;
        public List<Feature> Features { get; set; }
    }
}
