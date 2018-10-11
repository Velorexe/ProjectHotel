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
        private Bitmap _BackgroundBuffer { get; set; }
        private Bitmap _ForegroundBuffer { get; set; }
        private Bitmap _Wireframe { get; set; }
        private bool WireframeEnabled = false;

        Hotel MainHotel { get; set; }
        public SimulationForm(string fileLocation, Settings settings)
        {
            InitializeComponent();

            ImportLayout import = new ImportLayout();
            MainHotel = import.LayoutImport(fileLocation);
            MainHotel.Settings = settings;

            Graph.CreateGraph(MainHotel);

            _BackgroundBuffer = new Bitmap(MainHotel.Floors[0].Areas.Length * 60 + 1, MainHotel.Floors.Length * 55 + 1);
            _ForegroundBuffer = new Bitmap(MainHotel.Floors[0].Areas.Length * 60 + 1, MainHotel.Floors.Length * 55 + 1);
            _Wireframe = new Bitmap(MainHotel.Floors[0].Areas.Length * 60 + 1, MainHotel.Floors.Length * 55 + 1);

            DrawBackground(MainHotel);

            HotelEvents.HotelEventManager.Start();
            HotelEvents.HotelEventManager.HTE_Factor = (float)HteFactor.Value;

            TimerHTE.Interval = (int)HteFactor.Value;
            TimerHTE.Start();
        }

        private void SimulationForm_Load(object sender, EventArgs e)
        {

        }


        private void DrawBackground(Hotel hotel)
        {
            for (int i = 0; i < hotel.Floors.Length; i++)
            {
                for (int j = 0; j < hotel.Floors[i].Areas.Length; j++)
                {
                    using (Graphics g = Graphics.FromImage(_BackgroundBuffer))
                    {
                        g.DrawImage(hotel.Floors[i].Areas[j].Sprite, j * 60, (hotel.Floors.Length - 1 - i) * 55, 60, 55);
                        if (hotel.Floors[i].Areas[j].AreaType == EAreaType.Room)
                        {
                            Room tempRoom = (Room)hotel.Floors[i].Areas[j];
                            g.DrawString(tempRoom.Classification.ToString() + "⋆", this.Font, Brushes.Black, (j * 60), (hotel.Floors.Length - 1 - i) * 55);
                        }
                    }
                }
            }
            BackgroundLayer.Image = _BackgroundBuffer;
            BackgroundLayer.Size = _BackgroundBuffer.Size;
        }
        private void DrawForeground(Hotel hotel)
        {
            //What should be drawn on the foreground:
                //Room Occupied
                //Customers
                //Elevator
                //Cleaners


            for (int i = 0; i < hotel.Reception.Customers.Count; i++)
            {
                using (Graphics g = Graphics.FromImage(_BackgroundBuffer))
                {
                    g.DrawImage(hotel.Reception.Customers[i].Sprite, hotel.Reception.Customers[i].PositionX * 60, (hotel.Floors.Count() - 1 - hotel.Reception.Customers[i].PositionY) * 55 + (55 - hotel.Reception.Customers[i].Sprite.Height));
                }
                BackgroundLayer.Image = _BackgroundBuffer;
                BackgroundLayer.Size = _BackgroundBuffer.Size;
            }
        }


        private void TimerHTE_Tick(object sender, EventArgs e)
        {
            DrawForeground(MainHotel);
        }

        #region DEBUG

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
        private void DrawWireFrame(Hotel hotel)
        {
            if (WireframeEnabled == false)
            {
                WireframeEnabled = true;

                for (int i = 0; i < hotel.Floors.Length; i++)
                {
                    for (int j = 0; j < hotel.Floors[i].Areas.Length; j++)
                    {
                        using (Graphics g = Graphics.FromImage(_Wireframe))
                        {
                            if (hotel.Floors[i].Areas[j].AreaType != EAreaType.Hallway)
                            {
                                g.DrawRectangle(new Pen(Color.Red), j * 60, (hotel.Floors.Length - 1 - i - (hotel.Floors[i].Areas[j].Height - 1)) * 55, hotel.Floors[i].Areas[j].Width * 60, hotel.Floors[i].Areas[j].Height * 55);
                            }
                        }
                    }
                }
                using (Graphics g = Graphics.FromImage(_BackgroundBuffer))
                {
                    g.DrawImage(_Wireframe, 0, 0, _BackgroundBuffer.Width, _BackgroundBuffer.Height);
                }
                BackgroundLayer.Image = _Wireframe;
                BackgroundLayer.Size = _BackgroundBuffer.Size;
            }
            else
            {
                WireframeEnabled = false;
                BackgroundLayer.Image = _BackgroundBuffer;
            }
        }

        #endregion
    }
}
