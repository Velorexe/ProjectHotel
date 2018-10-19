using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelEvents;

namespace HotelSimulatie
{
    public partial class LiveStatistics : Form
    {
        private ISimulationForm Form { get; set; }
        public LiveStatistics(ISimulationForm Form)
        {
            InitializeComponent();
            Show();
            this.Form = Form;
        }

        public void UpdateStatistics()
        {
            Customers.Clear();
            Facilities.Clear();
            Cleaners.Clear();
            Rooms.Clear();

            foreach(Customer c in GlobalStatistics.Customers)
            {
                if (c.InArea != null)
                {
                    Customers.AppendText($"ID: {c.ID} \t {c.Name} \t {c.AssignedRoom.ID} \t Location: {c.InArea.ID}\n");
                }
                else
                {
                    Customers.AppendText($"ID: {c.ID} \t {c.Name} \t {c.AssignedRoom.ID}\n");
                }
            }
            foreach(Cleaner c in GlobalStatistics.Cleaners)
            {
                if (c.CurrentTask != null)
                {
                    Cleaners.AppendText($"ID: {c.CleanerID} \t {c.Name} \t Current Task: {c.CurrentTask.RoomToClean.Area.ID}\n");
                }
                else
                {
                    Cleaners.AppendText($"ID: {c.CleanerID} \t {c.Name}\n");
                }
            }
            foreach (Room c in GlobalStatistics.Rooms)
            {
                if (c.RoomOwner == null)
                {
                    Rooms.AppendText($"ID: {c.ID} \t Room {c.Classification} Stars \t EMPTY\n");
                }
                else
                {
                    Rooms.AppendText($"ID: {c.ID} \t Room {c.Classification} Stars \t {c.RoomOwner.ID}\n");
                }
            }
            foreach(Restaurant c in GlobalStatistics.Restaurants)
            {
                Facilities.AppendText($"ID: {c.ID} \t Eating Time: {c.EatingTime} \t {c.Capacity}\n");
            }
            foreach(Cinema c in GlobalStatistics.Cinemas)
            {
                Facilities.AppendText($"ID: {c.ID} \t Movie Time: {c.MovieTime}\n");
            }
            foreach(Fitness c in GlobalStatistics.FitnessCenters)
            {
                Facilities.AppendText($"ID: {c.ID} \t {c.Capacity}\n");
            }
        }

        private void LiveStatistics_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form.Statistics = null;
        }
    }
}
