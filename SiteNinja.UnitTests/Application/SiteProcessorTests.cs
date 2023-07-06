using SiteNinja.Application;
using SiteNinja.Models;
using Xunit;

namespace SiteNinja.UnitTests.Application
{
    public class SiteProcessorTests
    {
        [Fact]
        //[MemberData(nameof(Polygons))]
        //public void FindIntersections_ShouldReturnIntersections(List<Polygon> buildingLimits, List<Plateau> plateaus, List<Plateau> expectedIntersectionPlateaus)
        public void FindIntersections_ShouldReturnIntersections()
        {
            var buildingLimits = new List<Polygon>()
            {
                new Polygon(new List<Coordinate>(){ 
                    new Coordinate(3, 3), 
                    new Coordinate(8, 3), 
                    new Coordinate(8, 6), 
                    new Coordinate(3, 6) }),
            };

            var plateaus = new List<Plateau>()
            {
                new Plateau(
                    elevation: 3,
                    new Polygon(new List<Coordinate>(){
                        new Coordinate(5, 1),
                        new Coordinate(10, 1),
                        new Coordinate(10, 4),
                        new Coordinate(5, 4) }))
            };

            var solution = new List<Plateau>()
            {
                new Plateau(
                elevation: 3,
                new Polygon(new List<Coordinate>(){
                    new Coordinate(5, 3),
                    new Coordinate(8, 3),
                    new Coordinate(8, 4),
                    new Coordinate(5, 4)}))
            };


            var intersections = SiteProcessor.FindIntersections(buildingLimits, plateaus);

            Assert.Equal(intersections, solution);
        }

        public static IEnumerable<object[]> Polygons() => new[] {
            new object[] { TestBuildingLimits(), TestPlateaus(), new Plateau(
                elevation: 3,
                new Polygon(new List<Coordinate>(){
                    new Coordinate(5, 3),
                    new Coordinate(7, 3),
                    new Coordinate(7, 5),
                    new Coordinate(5, 4),
                    new Coordinate(5, 3) })) }
        };

        public static List<Polygon> TestBuildingLimits() => new List<Polygon>()
        {
            new Polygon(new List<Coordinate>(){ new Coordinate(3, 3), new Coordinate(3, 3), new Coordinate(7, 7), new Coordinate(3, 7), new Coordinate(3, 3) }),
        };

        public static List<Plateau> TestPlateaus() => new List<Plateau>()
        {
            new Plateau(
                elevation: 3,
                new Polygon(new List<Coordinate>(){
                    new Coordinate(5, 1),
                    new Coordinate(10, 1),
                    new Coordinate(10, 4),
                    new Coordinate(5, 4),
                    new Coordinate(5, 1) }))
        };

        //[Fact]
        //public void IsInsideEdge_ShouldReturnTrueForPointOnEdge()
        //{
        //    var edge1 = new Edge(new Coordinate(5, 4), new Coordinate(5, 1));

        //    var point = new Coordinate(5, 3);

        //    var isOnEgde = SiteProcessor.IsInsideEdge(edge1, point);

        //    Assert.True(isOnEgde);
        //}

        //[Fact]
        //public void GetEdgeIntersection_ShouldReturnCorrectIntersections()
        //{
        //    var edge1 = new Edge(new Coordinate(5, 4), new Coordinate(5, 1));
        //    var edge2 = new Edge(new Coordinate(3, 3), new Coordinate(7, 3));

        //    var intersection = SiteProcessor.GetEdgeIntersection(edge1, edge2);

        //    Assert.Equal(new Coordinate(5,3), intersection);
        //}
    }
}
