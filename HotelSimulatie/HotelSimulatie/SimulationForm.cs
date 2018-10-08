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
        private Bitmap _Wireframe = new Bitmap(1000, 1000);
        private bool WireframeEnabled = false;

        Hotel MainHotel { get; set; }
        public SimulationForm(string fileLocation)
        {
            InitializeComponent();
            ImportLayout import = new ImportLayout();
            MainHotel = import.LayoutImport(fileLocation);
            DrawBackground(MainHotel);
        }

        private void SimulationForm_Load(object sender, EventArgs e)
        {

        }

        private void DrawWireFrame(Hotel hotel)
        {
            if(WireframeEnabled == false)
            {
                WireframeEnabled = true;

                for (int i = 0; i < hotel.Floors.Count; i++)
                {
                    for (int j = 0; j < hotel.Floors[i].Areas.Count(); j++)
                    {
                        using (Graphics g = Graphics.FromImage(_Wireframe))
                        {
                            if (hotel.Floors[i].Areas[j].AreaType != EAreaType.Hallway)
                            {
                                g.DrawRectangle(new Pen(Color.Red), j * 60, i * 55, hotel.Floors[i].Areas[j].Width * 60, hotel.Floors[i].Areas[j].Height * 55);
                            }
                        }
                    }
                }
                using (Graphics g = Graphics.FromImage(_Buffer))
                {
                    g.DrawImage(_Wireframe, 0, 0, _Buffer.Width, _Buffer.Height);
                }
                BackgroundLayer.Image = _Wireframe;
            }
            else
            {
                WireframeEnabled = false;
                BackgroundLayer.Image = _Buffer;
            }
        }

        private void DrawBackground(Hotel hotel)
        {
            for (int i = 0; i < hotel.Floors.Count; i++)
            {
                for (int j = 0; j < hotel.Floors[i].Areas.Count(); j++)
                {
                    using (Graphics g = Graphics.FromImage(_Buffer))
                    {
                        g.DrawImage(hotel.Floors[i].Areas[j].Sprite, j * 60, i * 55, 60, 55);
                        if(hotel.Floors[i].Areas[j].GetType() == typeof(Room))
                        {
                            Room tempRoom = (Room)hotel.Floors[i].Areas[j];
                            g.DrawString(tempRoom.Classification.ToString() + "⋆", this.Font, Brushes.Black, (float)(j * 60), (float)(i * 55));
                        }
                    }
                }
            }
            BackgroundLayer.Image = _Buffer;
            BackgroundLayer.Size = _Buffer.Size;
        }

        private void WireFrameButton_Click(object sender, EventArgs e)
        {
            DrawWireFrame(MainHotel);
        }
    }
}
