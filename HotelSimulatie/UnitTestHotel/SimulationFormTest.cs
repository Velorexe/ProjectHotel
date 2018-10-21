using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;
using System.IO;

namespace UnitTestHotel
{
    [TestClass]
    public class SimulationFormTest
    {
        [TestMethod]
        public void TestifSimulationFormIsNotNull()
        {
            //arrange         
            SimulationForm SimulationFormTest;
            string path;
            Settings SettingsTest;

            //act     
            SettingsTest = new Settings();
            path = (Directory.GetCurrentDirectory() + @"..\..\..\HotelTestLayout.layout");
            SimulationFormTest = new SimulationForm(path, SettingsTest);


            //assert
            Assert.IsNotNull(SimulationFormTest);
        }       
    }
}
