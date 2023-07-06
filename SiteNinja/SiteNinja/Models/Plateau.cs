namespace SiteNinja.Models
{
    public class Plateau
    {
        public Plateau(double elevation, Polygon polygon)
        {
            Elevation = elevation;
            Polygon = polygon;
        }

        public double Elevation { get; }
        public Polygon Polygon { get; }

        public override bool Equals(object? obj)
        {
            try
            {
                if (obj is not Plateau comparePlateau) return false;

                var duplicates = Polygon.Coordinates.Intersect(comparePlateau.Polygon.Coordinates).ToList();
                var distinct = Polygon.Coordinates.Except(comparePlateau.Polygon.Coordinates).ToList();

                return Elevation == comparePlateau.Elevation
                    && duplicates.Count == Polygon.Coordinates.Count
                    && duplicates.Count == comparePlateau.Polygon.Coordinates.Count
                    && distinct.Count == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
