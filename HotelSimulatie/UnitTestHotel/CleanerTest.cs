using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;
using HotelEvents;

namespace UnitTestHotel
{
    [TestClass]
    public class CleanerTest
    {
        [TestMethod]
        public void TestCleanerRoute()
        {
            //arrange
            Cleaner cleaner;
            Room room;

            //act
            cleaner = new Cleaner();
            room = new Room();
            room.IsDirty = true;
            cleaner.CleanRoom(new CleanRoom()
            {
                RoomToClean = room.Node,
                TimeToClean = 5
            });
            cleaner.Move();

            //assert
            Assert.IsNotNull(cleaner.CurrentTask);
        }

        [TestMethod]
        public void TestPullIntsFromString()
        {
            //arrange
            Cleaner cleaner;

            //act
            cleaner = new Cleaner();
            HotelEvent Event = new HotelEvent();
            Event.EventType = HotelEventType.EVACUATE;
            cleaner.Notify(Event);

            //assert
            Assert.IsNotNull(cleaner.Path);
        }

    }
}
