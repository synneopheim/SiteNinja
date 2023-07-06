namespace SiteNinja.Models
{
    public class Polygon
    {
        public Polygon(List<Coordinate> coordinates)
        {
            Coordinates = coordinates;
        }

        public List<Coordinate> Coordinates { get; }

        public override bool Equals(object? obj)
        {
            try
            {
                if (obj is not Polygon comparePolygon) return false;

                var duplicates = Coordinates.Intersect(comparePolygon.Coordinates).ToList();
                var distinct = Coordinates.Except(comparePolygon.Coordinates).ToList();

                return duplicates.Count == Coordinates.Count
                    && duplicates.Count == comparePolygon.Coordinates.Count
                    && distinct.Count == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
