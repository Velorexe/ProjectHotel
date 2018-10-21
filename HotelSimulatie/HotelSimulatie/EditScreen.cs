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
    public partial class EditScreen : Form
    {
        private EAreaType AreaType { get; set; }
        private ISettingsScreen Form { get; set; }

        public int Value { get; set; }

        /// <summary>
        /// Creates a new EditScreen and set the Variables to the given Parameters (also fills the boxes with the give Parameters)
        /// </summary>
        /// <param name="AreaType">The AreaType that needs to be edited</param>
        /// <param name="Area">The Area that needs to be edited</param>
        /// <param name="Form">The ISettingsScreen form to apply eddits too</param>
        public EditScreen(EAreaType AreaType, IArea Area, ISettingsScreen Form)
        {
            InitializeComponent();

            this.AreaType = AreaType;
            this.Form = Form;

            if (AreaType == EAreaType.Cinema)
            {
                CinemaGroup.Visible = true;
                Width = CinemaGroup.Location.X * 3 + CinemaGroup.Width;
                ApplyButton.Location = new Point(CinemaGroup.Location.X, ApplyButton.Location.Y);
                ApplyButton.Width = CinemaGroup.Width;

                //FILLING DATA OF BOXES
                Cinema tempCinema = (Cinema)Area;
                CinemaID.Text = "" + tempCinema.ID;
                MovieTime.Value = tempCinema.MovieTime;
            }
            else if(AreaType == EAreaType.Restaurant)
            {
                RestaurantGroup.Visible = true;
                RestaurantGroup.Location = CinemaGroup.Location;
                Width = RestaurantGroup.Location.X * 3 + RestaurantGroup.Width;
                ApplyButton.Location = new Point(RestaurantGroup.Location.X, ApplyButton.Location.Y);
                ApplyButton.Width = RestaurantGroup.Width;

                //FILLING DATA OF BOXES
                Restaurant tempRestaurant = (Restaurant)Area;
                RestaurantID.Text = "" + tempRestaurant.ID;
                RestaurantTime.Value = tempRestaurant.EatingTime;
            }
            Show();
        }

        /// <summary>
        /// Apply's the Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if(AreaType == EAreaType.Cinema)
            {
                Value = (int)MovieTime.Value;
            }
            else if(AreaType == EAreaType.Restaurant)
            {
                Value = (int)RestaurantTime.Value;
            }

            Form.ApplyEdits(AreaType, Value, false);
            Close();
        }

        private void EditScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Not an efficient way to do this, but it works
            Form.ApplyEdits(AreaType, Value, true);
        }
    }
}
