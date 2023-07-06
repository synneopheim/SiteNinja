using SiteNinja.Models;

namespace SiteNinja.Application
{
    public static class SiteProcessor
    {
        /// <summary>
        /// Finds the intersecting polygons of the building limits and the plateau.
        /// Assumes that the building limits and plateaus are convex polugons.
        /// </summary>
        /// <param name="buildingLimits"></param>
        /// <param name="plateaus"></param>
        /// <returns></returns>
        public static List<Plateau> FindIntersections(List<Polygon> buildingLimits, List<Plateau> plateaus)
        {
            List<Plateau> intersections = new();

            foreach (var buildingLimitpolygon in buildingLimits)
            {
                foreach (var plateau in plateaus)
                {
                    var intersection = FindPolygonIntersection(buildingLimitpolygon, plateau.Polygon);
                    if (intersection != null)
                    {
                        intersections.Add(new Plateau(plateau.Elevation, intersection));
                    }
                }
            }

            return intersections;
        }


        private static Polygon? FindPolygonIntersection(Polygon polygon1, Polygon polygon2)
        {
            var corners = new List<Coordinate>();

            foreach (var coordinate in polygon1.Coordinates)
            {
                if (IsPointInsidePolygon(coordinate, polygon2))
                {
                    corners.Add(coordinate);
                }
            }

            foreach (var coordinate in polygon2.Coordinates)
            {
                if (IsPointInsidePolygon(coordinate, polygon1))
                {
                    corners.Add(coordinate);
                }
            }

            if (!corners.Any())
            {
                return null;
            }

            var intersectionPoints = GetIntersectionPoints(polygon1, polygon2);

            corners.AddRange(intersectionPoints);
            
            if (corners.Distinct().ToList().Count < 3)
            {
                // No intersection points or only collinear points (not a valid polygon)
                return null;
            }

            var orderedPolygonCoordinates = Order(corners);

            return new Polygon(orderedPolygonCoordinates);
        }

        public static bool IsPointInsidePolygon(Coordinate point, Polygon polygon)
        {
            int i;
            int j;

            bool result = false;
            for (i = 0, j = polygon.Coordinates.Count - 1; i < polygon.Coordinates.Count; j = i++)
            {
                if ((polygon.Coordinates[i].Y > point.Y) != (polygon.Coordinates[j].Y > point.Y) &&
                    (point.X < (polygon.Coordinates[j].X - polygon.Coordinates[i].X) * (point.Y - polygon.Coordinates[i].Y) / (polygon.Coordinates[j].Y - polygon.Coordinates[i].Y) + polygon.Coordinates[i].X))
                {
                    result = !result;
                }
            }

            return result;
        }

        private static List<Coordinate> GetIntersectionPoints(Polygon polygon1, Polygon polygon2)
        {
            var polygon1Edges = GetEdges(polygon1);
            var polygon2Edges = GetEdges(polygon2);

            var intersectionPoints = new List<Coordinate>();

            foreach (var edge1 in polygon1Edges)
            {
                foreach (var edge2 in polygon2Edges)
                {
                    var intersectionPoint = GetEdgeIntersection(edge1, edge2);

                    if (intersectionPoint is not null)
                    {
                        intersectionPoints.Add(intersectionPoint);
                    }
                }
            }

            return intersectionPoints;
        }

        public static List<Edge> GetEdges(Polygon polygon)
        {
            List<Edge> clipEdges = new();

            for (int i = 0; i < polygon.Coordinates.Count; i++)
            {
                int j = (i + 1) % polygon.Coordinates.Count;
                clipEdges.Add(new Edge(polygon.Coordinates[i], polygon.Coordinates[j]));
            }

            return clipEdges;
        }

        public static Coordinate? GetEdgeIntersection(
            Edge edge1,
            Edge edge2)
        {
            double dx1 = edge1.P2.X - edge1.P1.X;
            double dy1 = edge1.P2.Y - edge1.P1.Y;
            double dx2 = edge2.P2.X - edge2.P1.X;
            double dy2 = edge2.P2.Y - edge2.P1.Y;

            
            double denominator = dx1 * dy2 - dy1 * dx2;

            // Check if the lines are parallel or coincident
            if (Math.Abs(denominator) < 1e-10)
            {
                return null;
            }

            double t = ((edge2.P1.X - edge1.P1.X) * dy2 - (edge2.P1.Y - edge1.P1.Y) * dx2) / denominator;

            if (t < 0 || t > 1)
            {
                return null;
            }

            double x = edge1.P1.X + t * dx1;
            double y = edge1.P1.Y + t * dy1;

            // Check if intersection point is not outside the edge
            bool outsideEdge1 =
                x > Math.Max(edge1.P1.X, edge1.P2.X) 
                || x < Math.Min(edge1.P1.X, edge1.P2.X)
                || y > Math.Max(edge1.P1.Y, edge1.P2.Y)
                || x < Math.Min(edge1.P1.Y, edge1.P2.Y);

            bool outsideEdge2 =
                x > Math.Max(edge2.P1.X, edge2.P2.X)
                || x < Math.Min(edge2.P1.X, edge2.P2.X)
                || y > Math.Max(edge2.P1.Y, edge2.P2.Y)
                || x < Math.Min(edge2.P1.Y, edge2.P2.Y);

            if (outsideEdge1 || outsideEdge2)
            {
                return null;
            }

            return new Coordinate(x, y);
        }

        public static List<Coordinate> Order(List<Coordinate> corners)
        {
            double mX = 0;
            double mY = 0;

            foreach (var coordinate in corners)
            {
                mX += coordinate.X;
                mY += coordinate.Y;
            }

            mX /= corners.Count();
            mY /= corners.Count();

            return corners
                .OrderBy(v => Math.Atan2(v.Y - mY, v.X - mX))
                .ToList();
        }
    }
}
