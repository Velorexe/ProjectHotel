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
    public partial class SimulationForm : Form
    {
        private Bitmap _Buffer = new Bitmap(1000, 1000);
        Hotel MainHotel { get; set; }
        public SimulationForm(string fileLocation)
        {
            InitializeComponent();
            ImportLayout import = new ImportLayout();
            MainHotel = import.LayoutImport(fileLocation);
        }

        private void SimulationForm_Load(object sender, EventArgs e)
        {
            DrawBackground(MainHotel);
        }

        private void DrawBackground(Hotel hotel)
        {
            for (int i = 0; i < hotel.Floors.Count; i++)
            {
                for (int j = 0; j < hotel.Floors[i].Areas.Count(); j++)
                {
                    using (Graphics g = Graphics.FromImage(_Buffer))
                    {
                        if (hotel.Floors[i].Areas[j] is null)
                        {
                            g.DrawImage(HotelSimulatie.Properties.Resources.Room, hotel.Floors[i].Areas[j].PositionX * 60, hotel.Floors[i].Areas[j].PositionY * 55, 55, 60);
                        }
                        else
                        {
                            g.DrawImage(hotel.Floors[i].Areas[j].Sprite, hotel.Floors[i].Areas[j].PositionX * 60, hotel.Floors[i].Areas[j].PositionY * 55, 55, 60);
                        }
                    }
                }
            }
            BackgroundLayer.Image = _Buffer;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawBackground(MainHotel);
        }
    }
}
