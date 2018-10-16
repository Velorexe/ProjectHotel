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
            Form.ApplyEdits(AreaType, Value, true);
        }
    }
}
