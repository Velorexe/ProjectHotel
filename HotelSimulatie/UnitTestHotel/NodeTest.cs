using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;
using System.IO;

namespace UnitTestHotel
{
    [TestClass]
    public class NodeTest
    {
        [TestMethod]
        public void TestNodeLower()
        {
            //arrange
            Node node;
            Node node2;
            Node node3;
            IArea room;

            //act
            node = new Node();
            node2 = new Node();
            node3 = new Node();
            room = RoomFactory.Create(0, "Room", 0, 0, 5, 7, 1, 1);
            node.FillBottomNode(room, node2, node3, false);

            //assert
            Assert.IsNotNull(node.RightNode);
            Assert.IsNotNull(node.UpperNode);
        }

        [TestMethod]
        public void TestNodeLowerStairs()
        {
            //arrange
            Node node;
            Node node2;
            Node node3;
            IArea room;

            //act
            node = new Node();
            node2 = new Node();
            node3 = new Node();
            room = RoomFactory.Create(0, "Room", 0, 0, 5, 7, 1, 1);
            node.FillBottomNode(room, node2, node3, true);

            //assert
            Assert.IsNotNull(node.LeftNode);
            Assert.IsNotNull(node.UpperNode);
        }

        [TestMethod]
        public void TestNodeUpper()
        {
            //arrange
            Node node;
            Node node2;
            Node node3;
            IArea room;

            //act
            node = new Node();
            node2 = new Node();
            node3 = new Node();
            room = RoomFactory.Create(0, "Room", 0, 0, 5, 4, 1, 1);
            node.FillUpperNode(room, node2, node3, false);

            //assert
            Assert.IsNotNull(node.RightNode);
            Assert.IsNotNull(node.LowerNode);
        }

        [TestMethod]
        public void TestNodeUpperStairs()
        {
            //arrange
            Node node;
            Node node2;
            Node node3;
            IArea room;

            //act
            node = new Node();
            node2 = new Node();
            node3 = new Node();
            room = RoomFactory.Create(0, "Room", 0, 0, 5, 4, 1, 1);
            node.FillUpperNode(room, node2, node3, true);

            //assert
            Assert.IsNotNull(node.LowerNode);
            Assert.IsNotNull(node.LeftNode);
        }

        [TestMethod]
        public void TestNodeMoveable()
        {
            //arrange
            Node node;
            Node node2;
            Node node3;
            Node node4;
            IArea room;

            //act
            node = new Node();
            node2 = new Node();
            node3 = new Node();
            node4 = new Node();
            room = RoomFactory.Create(0, "Room", 0, 0, 5, 7, 1, 1);
            node.FillMoveableNode(room, node2, node3, node4, false);

            //assert
            Assert.IsNotNull(node.LowerNode);
            Assert.IsNotNull(node.UpperNode);
            Assert.IsNotNull(node.RightNode);
        }

        [TestMethod]
        public void TestNodeMoveableStairs()
        {
            //arrange
            Node node;
            Node node2;
            Node node3;
            Node node4;
            IArea room;

            //act
            node = new Node();
            node2 = new Node();
            node3 = new Node();
            node4 = new Node();
            room = RoomFactory.Create(0, "Room", 0, 0, 5, 7, 1, 1);
            node.FillMoveableNode(room, node2, node3, node4, true);

            //assert
            Assert.IsNotNull(node.LowerNode);
            Assert.IsNotNull(node.UpperNode);
            Assert.IsNotNull(node.LeftNode);
        }

        [TestMethod]
        public void TestNodeRoom()
        {
            //arrange
            Node node;
            Node node2;
            Node node3;
            IArea room;

            //act
            node = new Node();
            node2 = new Node();
            node3 = new Node();
            room = RoomFactory.Create(0, "Room", 0, 0, 5, 7, 1, 1);
            node.FillRoomNode(room, node2, node3);

            //assert
            Assert.IsNotNull(node.RightNode);
            Assert.IsNotNull(node.LeftNode);
        }
    }
}
