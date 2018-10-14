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
    public partial class SimulationForm : Form, ISimulationForm
    {
        private Bitmap _BackgroundBuffer { get; set; }
        private Bitmap _ForegroundBuffer { get; set; }
        private Bitmap _Wireframe { get; set; }

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

            _BackgroundBuffer = new Bitmap(Hotel.Floors[0].Areas.Length * 60 + 1, Hotel.Floors.Length * 55 + 1);
            _ForegroundBuffer = new Bitmap(Hotel.Floors[0].Areas.Length * 60 + 1, Hotel.Floors.Length * 55 + 1);
            _Wireframe = new Bitmap(Hotel.Floors[0].Areas.Length * 60 + 1, Hotel.Floors.Length * 55 + 1);

            DrawBackground();

            HotelEvents.HotelEventManager.Start();
            HotelEvents.HotelEventManager.HTE_Factor = (float)Hotel.Settings.HTEFactor;

            //WAIT BEFORE LOADING ALL THE DATA IN
            //BEFORE STARTING THE SIMULATION

            TimerHTE.Interval = (int)(1000 / Hotel.Settings.HTEFactor);
            TimerHTE.Start();

            DrawForeground();

            char t = Hotel.Elevator.GetElevatorInfo().Item1;
            int i = Hotel.Elevator.GetElevatorInfo().Item2;

            Hotel.Elevator.RequestElevator(4, 3);
            Hotel.Elevator.RequestElevator(1, 6);
            Hotel.Elevator.RequestElevator(1, 1);
            Hotel.Elevator.RequestElevator(-1, 5);
            Hotel.Elevator.RequestElevator(8, 3);
            Hotel.Elevator.RequestElevator(6, 9);
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
            BackgroundLayer.Size = _BackgroundBuffer.Size;
        }
        private void DrawForeground()
        {
            //What should be drawn on the foreground:
            //Cleaners
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
                for (int i = 0; i < Hotel.Reception.Customers.Count; i++)
                {
                    if (Hotel.Reception.Customers[i].IsInRoom == false)
                    {
                        g.DrawImage(Hotel.Reception.Customers[i].Sprite, Hotel.Reception.Customers[i].PositionX * 60, (Hotel.Floors.Count() - 1 - Hotel.Reception.Customers[i].PositionY) * 55 + (55 - Hotel.Reception.Customers[i].Sprite.Height));
                        BackgroundLayer.Image = _BackgroundBuffer;
                        BackgroundLayer.Size = _BackgroundBuffer.Size;
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
            foreach (Customer human in Hotel.Reception.Customers)
            {
                human.Move();
            }
            DrawForeground();
        }

        #region DEBUG

        private void WireFrameButton_Click(object sender, EventArgs e)
        {
            DrawWireFrame();
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
        private void DrawWireFrame()
        {
            if (WireframeEnabled == false)
            {
                WireframeEnabled = true;

                for (int i = 0; i < Hotel.Floors.Length; i++)
                {
                    for (int j = 0; j < Hotel.Floors[i].Areas.Length; j++)
                    {
                        using (Graphics g = Graphics.FromImage(_Wireframe))
                        {
                            if (Hotel.Floors[i].Areas[j].AreaType != EAreaType.Hallway)
                            {
                                g.DrawRectangle(new Pen(Color.Red, 5), j * 60, (Hotel.Floors.Length - 1 - i - (Hotel.Floors[i].Areas[j].Height - 1)) * 55, Hotel.Floors[i].Areas[j].Width * 60, Hotel.Floors[i].Areas[j].Height * 55);
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

        private void BackgroundLayer_MouseClick(object sender, MouseEventArgs e)
        {
            if(/*X CHECK*/(e.X >= 1 * 60 && e.X <= 2 * 60) && /*Y CHECK*/(e.Y >= (Hotel.Floors.Length - 1) * 55 && e.Y <= Hotel.Floors.Length * 55))
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
        }

        private void SimulationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Zoom(double ZoomLevel)
        {
            if(ZoomLevel == 1.0)
            {
                BackgroundLayer.SizeMode = PictureBoxSizeMode.Normal;
                BackgroundLayer.Size = _BackgroundBuffer.Size;
            }
            else if(ZoomLevel == 1.25)
            {
                BackgroundLayer.SizeMode = PictureBoxSizeMode.StretchImage;
                BackgroundLayer.Size = new Size((int)(_BackgroundBuffer.Size.Width * ZoomLevel), (int)(_BackgroundBuffer.Size.Height * ZoomLevel));
                this.Height = BackgroundLayer.Size.Height;
            }
            else if(ZoomLevel == 1.5)
            {
                BackgroundLayer.SizeMode = PictureBoxSizeMode.Normal;
                BackgroundLayer.Size = new Size((int)(BackgroundLayer.Size.Width * ZoomLevel), (int)(BackgroundLayer.Size.Height * ZoomLevel));
            }
        }
    }
}
