using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace HotelSimulatie
{
    public partial class ReceptionScreen : Form, ISettingsScreen
    {
        private ISimulationForm SimulationForm { get; set; }

        public ReceptionScreen(ISimulationForm SimulationForm)
        {
            InitializeComponent();
            this.SimulationForm = SimulationForm;
            Show();
            DefaultValuesButton_Click(new object(), new EventArgs());
            FillFacilities();
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
            return Double.Parse(result, CultureInfo.InvariantCulture);
        }

        private void FillFacilities()
        {
            RestaurantBox.DataSource = GlobalStatistics.Restaurants;
            RestaurantBox.DisplayMember = "ID";
            RestaurantBox.ValueMember = "ID";

            CinemasBox.DataSource = GlobalStatistics.Cinemas;
            CinemasBox.DisplayMember = "ID";
            CinemasBox.ValueMember = "ID";
        }

        private void RestaurantEditButton_Click(object sender, EventArgs e)
        {
            EditScreen tempScreen = new EditScreen(EAreaType.Restaurant, (Restaurant)RestaurantBox.SelectedItem, this);
            Enabled = false;
        }

        private void CinemaEditButton_Click(object sender, EventArgs e)
        {
            EditScreen tempScreen = new EditScreen(EAreaType.Cinema, (Cinema)CinemasBox.SelectedItem, this);
            Enabled = false;
        }

        public void ApplyEdits(EAreaType areaType, int Value, bool IsClosing)
        {
            if (IsClosing)
            {
                Enabled = true;
            }
            else
            {
                if (areaType == EAreaType.Restaurant)
                {
                    for (int i = 0; i < GlobalStatistics.Restaurants.Count; i++)
                    {
                        if (GlobalStatistics.Restaurants[i] == RestaurantBox.SelectedItem)
                        {
                            GlobalStatistics.Restaurants[i].EatingTime = Value;
                        }
                    }
                    RestaurantBox.DataSource = null;

                    RestaurantBox.DataSource = GlobalStatistics.Restaurants;
                    RestaurantBox.DisplayMember = "ID";
                    RestaurantBox.ValueMember = "ID";

                }
                else if (areaType == EAreaType.Cinema)
                {
                    for (int i = 0; i < GlobalStatistics.Cinemas.Count; i++)
                    {
                        if (GlobalStatistics.Cinemas[i] == CinemasBox.SelectedItem)
                        {
                            GlobalStatistics.Cinemas[i].MovieTime = Value;
                        }
                    }
                    CinemasBox.DataSource = null;

                    CinemasBox.DataSource = GlobalStatistics.Cinemas;
                    CinemasBox.DisplayMember = "ID";
                    CinemasBox.ValueMember = "ID";
                }
            }
        }
    }
}
