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
    public partial class SimulationForm : Form, HotelEvents.HotelEventListener
    {
        private Bitmap _Buffer { get; set; }
        private Bitmap _Wireframe { get; set; }
        private bool WireframeEnabled = false;

        Hotel MainHotel { get; set; }
        public SimulationForm(string fileLocation)
        {
            InitializeComponent();
            ImportLayout import = new ImportLayout();
            MainHotel = import.LayoutImport(fileLocation);
            _Buffer = new Bitmap(MainHotel.Floors[0].Areas.Count() * 60, MainHotel.Floors.Count * 55);
            _Wireframe = new Bitmap(MainHotel.Floors[0].Areas.Count() * 60, MainHotel.Floors.Count * 55);
            DrawBackground(MainHotel);
            HotelEvents.HotelEventManager.Start();
            HotelEvents.HotelEventManager.HTE_Factor = (float)HteFactor.Value;
            HotelEvents.HotelEventManager.Register(this);
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
                                if (hotel.Floors[i].Areas[j].GetType() == typeof(Room))
                                {
                                    Room tempRoom = (Room)hotel.Floors[i].Areas[j];
                                    g.DrawString(tempRoom.Classification.ToString() + "⋆", this.Font, Brushes.Black, (float)(j * 60), (float)(i * 55));
                                }
                            }
                        }
                    }
                }
                using (Graphics g = Graphics.FromImage(_Buffer))
                {
                    g.DrawImage(_Wireframe, 0, 0, _Buffer.Width, _Buffer.Height);
                }
                BackgroundLayer.Image = _Wireframe;
                BackgroundLayer.Size = _Buffer.Size;
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
                        if (hotel.Floors[i].Areas[j] is null)
                        {
                            g.DrawImage(HotelSimulatie.Properties.Resources.Room, j * 60, i* 55, 60, 55);
                            g.DrawImage(Sprites.Room, hotel.Floors[i].Areas[j].PositionX * 60, hotel.Floors[i].Areas[j].PositionY * 55, 55, 60);
                        }
                        else
                        {
                            g.DrawImage(hotel.Floors[i].Areas[j].Sprite, j * 60, i * 55, 60, 55);
                        }
                        g.DrawImage(hotel.Floors[i].Areas[j].Sprite, j * 60, i * 55, 60, 55);
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

        private void HteFactor_ValueChanged(object sender, EventArgs e)
        {
            HotelEvents.HotelEventManager.HTE_Factor = (float)HteFactor.Value;
        }

        private void DebugCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DebugCheckBox.Checked)
            {
                DebugGroup.Visible = true;
            }
            else
            {
                DebugGroup.Visible = false;
            }
        }

        public void Notify(HotelEvents.HotelEvent HotelEvent)
        {
            if (DebugCheckBox.Checked)
            {
                MessageBox.Show(HotelEvent.EventType.ToString() + " : " + HotelEvent.Message + " @ " + HotelEvent.Time);
            }
        }
    }
}
