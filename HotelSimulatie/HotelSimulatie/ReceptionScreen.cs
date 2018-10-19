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
        private bool IsStarting { get; set; } = true;
        private ISimulationForm SimulationForm { get; set; }

        public ReceptionScreen(ISimulationForm SimulationForm)
        {
            InitializeComponent();
            this.SimulationForm = SimulationForm;
            Show();
            FillSettings();
            FillFacilities();
            HighlightAllFacilities();
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
                TimeBeforeDeath = (int)TimeBeforeDeath.Value,
                StairCase = (int)StairTime.Value
            };
            SimulationForm.ApplySettings(tempSettings);
        }

        private void FillSettings()
        {
            ZoomLevel.Text = $"x {Hotel.Settings.ZoomLevel}.0";
            SimulationSpeed.Text = $"x {Hotel.Settings.ZoomLevel}.0";

            CleaningTime.Value = Hotel.Settings.CleaningTime;
            TimeBeforeDeath.Value = Hotel.Settings.TimeBeforeDeath;
            StairTime.Value = Hotel.Settings.StairCase;
        }

        private void DefaultValuesButton_Click(object sender, EventArgs e)
        {
            Settings tempSettings = new Settings();

            ZoomLevel.Text = $"x {tempSettings.ZoomLevel}.0";
            SimulationSpeed.Text = $"x {tempSettings.ZoomLevel}.0";

            CleaningTime.Value = tempSettings.CleaningTime;
            TimeBeforeDeath.Value = tempSettings.TimeBeforeDeath;
            StairTime.Value = tempSettings.StairCase;
        }

        private double ParseLevels(string Level)
        {
            string result = Level.Replace("x ", "");
            return double.Parse(result, CultureInfo.InvariantCulture);
        }

        private void FillFacilities()
        {
            RestaurantBox.DataSource = GlobalStatistics.Restaurants;
            RestaurantBox.DisplayMember = "ID";
            RestaurantBox.ValueMember = "ID";

            CinemasBox.DataSource = GlobalStatistics.Cinemas;
            CinemasBox.DisplayMember = "ID";
            CinemasBox.ValueMember = "ID";

            RoomsBox.DataSource = GlobalStatistics.Rooms;
            RoomsBox.DisplayMember = "ID";
            RoomsBox.ValueMember = "ID";

            //CUSTOMER
            if (GlobalStatistics.Customers.Count != 0)
            {
                CustomerBox.DataSource = GlobalStatistics.Customers;
                CustomerBox.DisplayMember = "ID";
                CustomerBox.ValueMember = "ID";

                CustomerName.Text = ((Customer)CustomerBox.SelectedItem).Name;
                AssignedRoom.Text = ((Customer)CustomerBox.SelectedItem).AssignedRoom.ID.ToString();
                CurrentActivity.Text = ((Customer)CustomerBox.SelectedItem).Status.ToString();
            }

            IsStarting = false;
        }

        private void RestaurantEditButton_Click(object sender, EventArgs e)
        {
            EditScreen tempScreen = new EditScreen(EAreaType.Restaurant, (Restaurant)RestaurantBox.SelectedItem, this);
            Enabled = false;
        }

        private void HighlightAllFacilities()
        {
            SimulationForm.HighlightFacility(new IArea[] { (IArea)CinemasBox.SelectedItem, (IArea)RestaurantBox.SelectedItem, (IArea)RoomsBox.SelectedItem });
        }

        private void CinemaEditButton_Click(object sender, EventArgs e)
        {
            EditScreen tempScreen = new EditScreen(EAreaType.Cinema, (Cinema)CinemasBox.SelectedItem, this);
            Enabled = false;
        }

        private void RoomViewButton_Click(object sender, EventArgs e)
        {
            EditScreen tempScreen = new EditScreen(EAreaType.Room, (IArea)RoomsBox.SelectedItem, this);
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


        private void RestaurantBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsStarting && RestaurantBox.SelectedItem != null)
            {
                HighlightAllFacilities();
            }
        }

        private void CinemasBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsStarting && CinemasBox.SelectedItem != null)
            {
                HighlightAllFacilities();
            }
        }

        private void RoomsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsStarting && RoomsBox.SelectedItem != null)
            {
                HighlightAllFacilities();
            }
        }

        private void CustomerApplyButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GlobalStatistics.Customers.Count; i++)
            {
                if (GlobalStatistics.Customers[i] == CustomerBox.SelectedItem)
                {
                    GlobalStatistics.Customers[i].Name = CustomerName.Text;
                }
            }
            CustomerBox.DataSource = null;

            if (GlobalStatistics.Customers.Count != 0)
            {
                CustomerBox.DataSource = GlobalStatistics.Customers;
                CustomerBox.DisplayMember = "ID";
                CustomerBox.ValueMember = "ID";

                CustomerName.Text = ((Customer)CustomerBox.SelectedItem).Name;
                AssignedRoom.Text = ((Customer)CustomerBox.SelectedItem).AssignedRoom.ID.ToString();
                CurrentActivity.Text = ((Customer)CustomerBox.SelectedItem).Status.ToString();
            }
        }

        private void CustomerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CustomerBox.SelectedItem != null)
            {
                CustomerName.Text = ((Customer)CustomerBox.SelectedItem).Name;
                AssignedRoom.Text = ((Customer)CustomerBox.SelectedItem).AssignedRoom.ID.ToString();
                CurrentActivity.Text = ((Customer)CustomerBox.SelectedItem).Status.ToString();
            }
        }

        private void LiveStatisticsButton_Click(object sender, EventArgs e)
        {
            if (SimulationForm.Statistics == null)
            {
                LiveStatistics Screen = new LiveStatistics(SimulationForm);
                SimulationForm.Statistics = Screen;
            }
            else
            {
                SimulationForm.Statistics.Close();
            }
        }
    }
}
