namespace SiteNinja.Models
{
    public class Geometry
    {
        public Geometry(List<List<List<double>>> coordinates)
        {
            Coordinates = coordinates;
        }

        public GeometryType Type => GeometryType.Geometry;

        /// <summary>
        /// Coordinates.
        /// 
        /// The innermost list of doubles represents one coordinate, [2.346346, 5.2]
        /// </summary>
        public List<List<List<double>>> Coordinates { get; set; }
    }
}
