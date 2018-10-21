using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;

namespace UnitTestHotel
{

    [TestClass]
    public class FactoryTest
    {       
   

        [TestMethod]
        public void TestIfCustomerFactoryIsWorking()
        {
            //arrange         
            Customer customer;

            //act     
            customer = (Customer)HumanFactory.CreateHuman(EHumanType.Customer);

            //assert
            Assert.IsNotNull(customer);
        }

        [TestMethod]
        public void TestIfCleanerFactoryIsWorking()
        {
            //arrange         
            Cleaner cleaner;

            //act     
            cleaner = (Cleaner)HumanFactory.CreateHuman(EHumanType.Cleaner);

            //assert
            Assert.IsNotNull(cleaner);
        }

        [TestMethod]
        public void TestIfHallwayFactoryIsWorking()
        {
            //arrange
            IArea hallway;

            //act
            hallway = RoomFactory.Create(0, "Hallway", 0, 0, 5, 7, 1, 1);

            //assert
            Assert.IsNotNull(hallway);
        }

        [TestMethod]
        public void TestIfRoomFactoryIsWorking()
        {
            //arrange
            IArea room;

            //act
            room = RoomFactory.Create(0, "Room", 0, 0, 5, 7, 1, 1);

            //assert
            Assert.IsNotNull(room);
        }

        [TestMethod]
        public void TestIfCinemaFactoryIsWorking()
        {
            //arrange
            IArea cinema;

            //act
            cinema = RoomFactory.Create(0, "Cinema", 0, 0, 5, 7, 1, 1);

            //assert
            Assert.IsNotNull(cinema);
        }

        [TestMethod]
        public void TestIfFitnessFactoryIsWorking()
        {
            //arrange
            IArea fitness;

            //act
            fitness = RoomFactory.Create(0, "Fitness", 0, 0, 5, 7, 1, 1);

            //assert
            Assert.IsNotNull(fitness);
        }

        [TestMethod]
        public void TestIfReceptionactoryIsWorking()
        {
            //arrange
            IArea reception;

            //act
            reception = RoomFactory.Create(0, "Reception", 0, 0, 5, 7, 1, 1);

            //assert
            Assert.IsNotNull(reception);
        }

        [TestMethod]
        public void TestIfRestaurantFactoryIsWorking()
        {
            //arrange
            IArea restaurant;

            //act
            restaurant = RoomFactory.Create(0, "Restaurant", 0, 0, 5, 7, 1, 1);

            //assert
            Assert.IsNotNull(restaurant);
        }

        [TestMethod]
        public void TestIfStaircaseFactoryIsWorking()
        {
            //arrange
            IArea stairs;

            //act
            stairs = RoomFactory.Create(0, "Staircase", 0, 0, 5, 7, 1, 1);

            //assert
            Assert.IsNotNull(stairs);
        }

        [TestMethod]
        public void TestIfElevatorShaftFactoryIsWorking()
        {
            //arrange
            IArea elevator;

            //act
            elevator = RoomFactory.Create(0, "ElevatorShaft", 0, 0, 5, 7, 1, 1);

            //assert
            Assert.IsNotNull(elevator);
        }
    }
}
