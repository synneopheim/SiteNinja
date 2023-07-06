using SiteNinja.Models;

namespace SiteNinja.Validation
{
    public static class RequestValidator
    {
        internal static List<Polygon> ValidateAndMapBuildingLimits(FeatureCollection building_limits)
        {
            if (building_limits.Features.Count > 1)
            {
                throw new NotImplementedException("The application cannot handle more than one feature to the building limits yet.");
            }

            var geometry = building_limits.Features
                .FirstOrDefault()?
                .Geometry;

            if (geometry is null) 
            {
                throw new ArgumentException("The request body is missing building limit geometry.");
            }

            return MapToPolygons(geometry.Coordinates, "building limits");
        }

        internal static List<Plateau> ValidateAndMapPlateaus(FeatureCollection height_plateaus)
        {
            return height_plateaus.Features.Select(MapToPlateau).ToList();
            
        }

        private static Plateau MapToPlateau(Feature feature)
        {
            if (!feature.Properties.TryGetValue("elevation", out var elevation))
            {
                throw new ArgumentException("Each plateau must have elevation.");
            }

            var polygons = MapToPolygons(feature.Geometry.Coordinates, "height plateaus");

            if (polygons.Count != 1)
            {
                throw new ArgumentException("This application only supports height plateaus consisting of one polygon.");
            }

            return new Plateau(elevation, polygons.First());
        }

        private static List<Polygon> MapToPolygons(List<List<List<double>>> coordinates, string source)
        {
            try
            {
                var polygons = coordinates.Select(MapToPolygon);

                if (polygons is not null && polygons.Any())
                {
                    return polygons.ToList();
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch 
            {
                throw new ArgumentException($"Unable to parse coordinates of {source} to a polygon.");
            }

            throw new ArgumentException($"The {source} did not contain any polygons.");
        }

        private static Polygon MapToPolygon(List<List<double>> coordinates)
        {
            if (coordinates.Any(coordinate => coordinate.Count != 2))
            {
                throw new NotImplementedException("This application does not support coordinates with other than two dimensions.");
            }

            var polygonCoordinates = coordinates.Select(c => new Coordinate(c.First(), c.Last()));
            var polygon = new Polygon(polygonCoordinates.ToList());

            ValidateOrThrowException(polygon); 

            return new Polygon(polygonCoordinates.ToList());
        }

        private static void ValidateOrThrowException(Polygon polygon)
        {
            if (polygon.Coordinates.Count < 4)
            {
                throw new ArgumentException("Each polygon must be a list of at least 4 coordinates.");
            }

            var firstCoordinate = polygon.Coordinates.First();
            var lastCoordinate = polygon.Coordinates.Last();

            if (!firstCoordinate.Equals(lastCoordinate))
            {
                throw new ArgumentException("Eeach polygon must be a closed ring of coordinates.");
            }
        }
    }
}
