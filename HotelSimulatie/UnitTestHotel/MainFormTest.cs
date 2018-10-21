using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;

namespace UnitTestHotel
{
    [TestClass]
    public class MainFormTest
    {
        [TestMethod]
        public void TestifMainFormIsNotNull()
        {
            //arrange         
            MainForm MainFormTest;

            //act       
            MainFormTest = new MainForm();


            //assert
            Assert.IsNotNull(MainFormTest);
        }
    }
}
