using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;
using System.IO;

namespace UnitTestHotel
{
    [TestClass]
    public class EditScreenTest
    {
        [TestMethod]
        public void TestIfEditScreenIsWorking()
        {
            //arrange
            SimulationForm SimulationFormTest;
            string path;
            Settings SettingsTest;
            EditScreen EditScreenTest;

            //act
            SettingsTest = new Settings();
            path = (Directory.GetCurrentDirectory() + @"..\..\..\HotelTestLayout.layout");
            SimulationFormTest = new SimulationForm(path, SettingsTest);

            EditScreenTest = new EditScreen(EAreaType.Reception, null, null);


            //assert
            Assert.IsNotNull(EditScreenTest);
        }
    }
}
