using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;
using System.IO;

namespace UnitTestHotel
{
    [TestClass]
    public class LiveStaticticsTest
    {
        [TestMethod]
        public void TestIfLiveStaticticsIsWorking()
        {
            //arrange         
            SimulationForm SimulationFormTest;
            string path;
            Settings SettingsTest;
            LiveStatistics LiveStatisticsTest;

            //act     
            SettingsTest = new Settings();
            path = (Directory.GetCurrentDirectory() + @"..\..\..\HotelTestLayout.layout");
            SimulationFormTest = new SimulationForm(path, SettingsTest);

            LiveStatisticsTest = new LiveStatistics(SimulationFormTest);


            //assert
            Assert.IsNotNull(LiveStatisticsTest);
        }
    }
}
