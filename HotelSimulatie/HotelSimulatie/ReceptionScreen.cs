using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSimulatie
{
    public partial class ReceptionScreen : Form
    {
        private ISimulationForm SimulationForm { get; set; }

        public ReceptionScreen(ISimulationForm SimulationForm)
        {
            InitializeComponent();
            this.SimulationForm = SimulationForm;
            this.Show();
        }

        private void ReceptionScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            SimulationForm.PauseSimulation(true);
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            Settings tempSettings = new Settings()
            {
                ZoomLevel = ParseLevels(ZoomLevel.Text),
                HTEFactor = ParseLevels(SimulationSpeed.Text),
                CleaningTime = (int)CleaningTime.Value,
                EatingTime = (int)EatingTime.Value,
                StairCase = (int)StairTime.Value
            };
            SimulationForm.ApplySettings(tempSettings);
        }

        private void DefaultValuesButton_Click(object sender, EventArgs e)
        {
            Settings tempSettings = new Settings();

            ZoomLevel.Text = $"x {tempSettings.ZoomLevel}.0";
            SimulationSpeed.Text = $"x {tempSettings.ZoomLevel}.0";

            CleaningTime.Value = tempSettings.CleaningTime;
            EatingTime.Value = tempSettings.EatingTime;
            StairTime.Value = tempSettings.StairCase;
        }

        private double ParseLevels(string Level)
        {
            string result = Level.Replace("x ", "");
            return Convert.ToDouble(result);
        }
    }
}
