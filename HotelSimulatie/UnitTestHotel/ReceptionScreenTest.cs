using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;
using System.IO;

namespace UnitTestHotel
{
    [TestClass]
    public class ReceptionScreenTest
    {
        [TestMethod]
        public void TestIfReceptionScreenIsWorking()
        {
            //arrange         
            SimulationForm SimulationFormTest;
            string path;
            Settings SettingsTest;
            ReceptionScreen receptionScreenTest;

            //act     
            SettingsTest = new Settings();
            path = (Directory.GetCurrentDirectory() + @"..\..\..\HotelTestLayout.layout");
            SimulationFormTest = new SimulationForm(path, SettingsTest);

            receptionScreenTest = new ReceptionScreen(SimulationFormTest);


            //assert
            Assert.IsNotNull(receptionScreenTest);
        }
    }
}
