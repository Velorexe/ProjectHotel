using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;

namespace UnitTestHotel
{
    [TestClass]
    public class RouteTest
    {
        [TestMethod]
        public void TestRoute()
        {
            //arrange
            Route route = new Route();
            
            //act
            route = Graph.QuickestRoute(Hotel.Floors[5].Areas[2].Node, Hotel.Floors[3].Areas[5].Node, true, true);            

            //assert
            Assert.IsNotNull(route);
        }

        [TestMethod]
        public void TestRouteElevator()
        {
            //arrange
            Route route = new Route();

            //act
            route = Graph.GetElevatorRoute(Hotel.Floors[5].Areas[2].Node, Hotel.Floors[3].Areas[5].Node);

            //assert
            Assert.IsNotNull(route);
        }

        [TestMethod]
        public void TestNearestFacilities()
        {
            //arrange
            Node node = new Node();

            //act
            node = Graph.NearestFacility(Hotel.Floors[5].Areas[2].Node, EAreaType.Restaurant);

            //assert
            Assert.IsNotNull(node);
        }

        [TestMethod]
        public void TestSearchNode()
        {
            //arrange
            Node node = new Node();
            Room room = new Room();

            //act
            node = Graph.SearchNode(room);

            //assert
            Assert.IsNotNull(node);
        }

    }
}
