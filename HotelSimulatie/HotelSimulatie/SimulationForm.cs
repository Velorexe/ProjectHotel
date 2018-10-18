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
    public partial class SimulationForm : Form, ISimulationForm
    {
        private Bitmap _BackgroundBuffer { get; set; }
        private Bitmap _ForegroundBuffer { get; set; }
        private Bitmap _Wireframe { get; set; }

        public LiveStatistics Statistics { get; set; }

        private ReceptionScreen ReceptionScreen { get; set; }

        private bool WireframeEnabled = false;
        private bool Paused = false;

        public SimulationForm(string fileLocation, Settings settings)
        {
            InitializeComponent();

            ImportLayout import = new ImportLayout();
            import.LayoutImport(fileLocation);

            Hotel.Settings = settings;


            Graph.CreateGraph();

            Hotel.Reception.HireCleaners(Hotel.Settings.CleanerAmount);

            _BackgroundBuffer = new Bitmap(Hotel.Floors[0].Areas.Length * 60 + 1, Hotel.Floors.Length * 55 + 1);
            _ForegroundBuffer = new Bitmap(Hotel.Floors[0].Areas.Length * 60 + 1, Hotel.Floors.Length * 55 + 1);
            _Wireframe = new Bitmap(Hotel.Floors[0].Areas.Length * 60 + 1, Hotel.Floors.Length * 55 + 1);

            DrawBackground();

            HotelEventManager.Start();
            HotelEventManager.HTE_Factor = (float)Hotel.Settings.HTEFactor;

            TimerHTE.Interval = (int)(1000 / Hotel.Settings.HTEFactor);
            TimerHTE.Start();

            DrawForeground();

            BackgroundLayer.Size = _BackgroundBuffer.Size;
            Size = new Size(BackgroundLayer.Size.Width + BackgroundLayer.Location.X * 3, BackgroundLayer.Size.Height + BackgroundLayer.Location.Y * 3);

        }

        private void SimulationForm_Load(object sender, EventArgs e)
        {

        }


        private void DrawBackground()
        {
            using (Graphics g = Graphics.FromImage(_BackgroundBuffer))
            {
                for (int i = 0; i < Hotel.Floors.Length; i++)
                {
                    for (int j = 0; j < Hotel.Floors[i].Areas.Length; j++)
                    {
                        g.DrawImage(Hotel.Floors[i].Areas[j].Sprite, j * 60, (Hotel.Floors.Length - 1 - i) * 55, 60, 55);
                        if (Hotel.Floors[i].Areas[j].AreaType == EAreaType.Room)
                        {
                            Room tempRoom = (Room)Hotel.Floors[i].Areas[j];
                            g.DrawString(tempRoom.Classification.ToString() + "⋆", this.Font, Brushes.Black, (j * 60), (Hotel.Floors.Length - 1 - i) * 55);
                        }
                    }
                }
            }
            DrawFacilities();
            BackgroundLayer.Image = _BackgroundBuffer;
        }
        private void DrawForeground()
        {
            _ForegroundBuffer.Dispose();
            DrawBackground();

            using (Graphics g = Graphics.FromImage(_BackgroundBuffer))
            {
                g.DrawImage(Hotel.Elevator.Sprite, Hotel.Elevator.PositionX * 60, (Hotel.Floors.Count() - 1 - Hotel.Elevator.PositionY) * 55);
                for (int i = 0; i < Hotel.Floors.Length; i++)
                {
                    for (int j = 0; j < Hotel.Floors[i].Areas.Length; j++)
                    {
                        if(Hotel.Floors[i].Areas[j].AreaType == EAreaType.Room)
                        {
                            Room tempRoom = (Room)Hotel.Floors[i].Areas[j];
                            if(tempRoom.RoomOwner != null)
                            {
                                g.DrawImage(tempRoom.Occupied, j * 60, (Hotel.Floors.Length - 1 - i) * 55, 60, 55);
                            }
                        }
                    }
                }
                for (int i = 0; i < GlobalStatistics.Customers.Count; i++)
                {
                    if (GlobalStatistics.Customers[i].IsVisible == true)
                    {
                        g.DrawImage(GlobalStatistics.Customers[i].Sprite, GlobalStatistics.Customers[i].PositionX * 60, (Hotel.Floors.Count() - 1 - GlobalStatistics.Customers[i].PositionY) * 55 + (55 - GlobalStatistics.Customers[i].Sprite.Height));
                        BackgroundLayer.Image = _BackgroundBuffer;
                    }
                }
                for (int i = 0; i < GlobalStatistics.Cleaners.Count; i++)
                {
                    if(GlobalStatistics.Cleaners[i].IsVisible == true)
                    {
                        g.DrawImage(GlobalStatistics.Cleaners[i].Sprite, GlobalStatistics.Cleaners[i].PositionX * 60, (Hotel.Floors.Count() - 1 - GlobalStatistics.Cleaners[i].PositionY) * 55 + (55 - GlobalStatistics.Cleaners[i].Sprite.Height));
                        BackgroundLayer.Image = _BackgroundBuffer;
                    }
                }
            }
        }


        private void DrawFacilities()
        {
            using (Graphics g = Graphics.FromImage(_BackgroundBuffer))
            {
                for (int i = 0; i < Hotel.Floors.Length; i++)
                {
                    for (int j = 0; j < Hotel.Floors[i].Areas.Length; j++)
                    {
                        if (Hotel.Floors[i].Areas[j].AreaType != EAreaType.Room && Hotel.Floors[i].Areas[j].AreaType != EAreaType.Hallway)
                        {
                            for (int k = 0; k < Hotel.Floors[i].Areas[j].Width; k++)
                            {
                                for (int m = 0; m < Hotel.Floors[i].Areas[j].Height; m++)
                                {
                                    g.DrawImage(Hotel.Floors[i].Areas[j].Sprite, (j + k) * 60, (Hotel.Floors.Length - 1 - i - m) * 55, 60, 55);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void TimerHTE_Tick(object sender, EventArgs e)
        {
            Hotel.Elevator.Move();
            for (int i = 0; i < GlobalStatistics.Customers.Count; i++)
            {
                GlobalStatistics.Customers[i].Move();
            }
            for (int i = 0; i < GlobalStatistics.Cleaners.Count; i++)
            {
                GlobalStatistics.Cleaners[i].Move();
            }
            if(Statistics != null)
            {
                Statistics.UpdateStatistics();
            }
            DrawForeground();
        }

        private void BackgroundLayer_MouseClick(object sender, MouseEventArgs e)
        {
            if(/*X CHECK*/(e.X >= 1 * 60  * Hotel.Settings.ZoomLevel && e.X <= 2 * 60 * Hotel.Settings.ZoomLevel) && /*Y CHECK*/(e.Y >= (Hotel.Floors.Length - 1) * 55 * Hotel.Settings.ZoomLevel && e.Y <= Hotel.Floors.Length * 55 * Hotel.Settings.ZoomLevel))
            {
                PauseSimulation(false);
            }
        }

        public void PauseSimulation(bool IsClosing)
        {
            HotelEvents.HotelEventManager.Pauze();
            if (Paused)
            {
                TimerHTE.Start();
                if (!IsClosing)
                {
                    ReceptionScreen.Close();
                }
                Paused = false;
            }
            else
            {
                TimerHTE.Stop();
                ReceptionScreen = new ReceptionScreen(this);
                Paused = true;
            }
        }

        public void ApplySettings(Settings settings)
        {
            Hotel.Settings = settings;
            TimerHTE.Interval = (int)(1000 / Hotel.Settings.HTEFactor);
            Zoom(Hotel.Settings.ZoomLevel);
            HotelEventManager.HTE_Factor = (float)Hotel.Settings.HTEFactor;
        }

        private void SimulationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Zoom(double ZoomLevel)
        {
            if(ZoomLevel == 1.0)
            {
                BackgroundLayer.Size = _BackgroundBuffer.Size;
                BackgroundLayer.SizeMode = PictureBoxSizeMode.StretchImage;
                Size = new Size(BackgroundLayer.Size.Width + BackgroundLayer.Location.X * 3, BackgroundLayer.Size.Height + BackgroundLayer.Location.Y * 3);
            }
            else if(ZoomLevel == 1.25)
            {
                BackgroundLayer.Size = new Size((int)(_BackgroundBuffer.Size.Width * ZoomLevel), (int)(_BackgroundBuffer.Size.Height * ZoomLevel));
                BackgroundLayer.SizeMode = PictureBoxSizeMode.StretchImage;
                Size = new Size(BackgroundLayer.Size.Width + BackgroundLayer.Location.X * 3, BackgroundLayer.Size.Height + BackgroundLayer.Location.Y * 3);
            }
            else if(ZoomLevel == 1.5)
            {
                BackgroundLayer.Size = new Size((int)(BackgroundLayer.Image.Width * ZoomLevel), (int)(BackgroundLayer.Image.Height * ZoomLevel));
                BackgroundLayer.SizeMode = PictureBoxSizeMode.StretchImage;
                Size = new Size(BackgroundLayer.Size.Width + BackgroundLayer.Location.X * 3, BackgroundLayer.Size.Height + BackgroundLayer.Location.Y * 3);
            }
            else if (ZoomLevel == 2)
            {
                BackgroundLayer.Size = new Size((int)(BackgroundLayer.Image.Width * ZoomLevel), (int)(BackgroundLayer.Image.Height * ZoomLevel));
                BackgroundLayer.SizeMode = PictureBoxSizeMode.StretchImage;
                Size = new Size(BackgroundLayer.Size.Width + BackgroundLayer.Location.X * 3, BackgroundLayer.Size.Height + BackgroundLayer.Location.Y * 3);
            }
        }

        public void HighlightFacility(IArea[] areas)
        {
            DrawBackground();
            DrawForeground();
            BackgroundLayer.Image = _BackgroundBuffer;
            using (Graphics g = Graphics.FromImage(_BackgroundBuffer))
            {
                foreach (IArea area in areas)
                {
                    Pen p = new Pen(Color.Red)
                    {
                        Width = 5
                    };
                    g.DrawRectangle(new Pen(Color.Red, 5), area.PositionX * 60, (Hotel.Floors.Length - 1 - area.PositionY - (area.Height - 1)) * 55, area.Width * 60, area.Height * 55);
                }
            }
            BackgroundLayer.Invalidate();
        }
    }
}
