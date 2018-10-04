using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace HotelSimulatie
{
    class Reception : IArea, HotelEvents.HotelEventListener
    {
        public EAreaType AreaType { get; set; } = EAreaType.Reception;
        public int Height { get; set; } = 1;
        public int Width { get; set; } = 1;
        public int PositionX { get; set; } = 1;
        public int PositionY { get; set; }
<<<<<<< HEAD:HotelSimulatie/HotelSimulatie/Classes/Reception.cs
        public Bitmap Sprite { get; set; } = Sprites.Reception;

        public void Create(EAreaType areaType, int capacity, int classification, int positionX, int positionY, int width, int height, Bitmap sprite)
=======
        public Bitmap Sprite { get; set; } = Sprites.ReceptionBar;

        public void Create(EAreaType areaType, int capacity, int classification, int positionX, int positionY, int width, int height)
>>>>>>> 061aedc796ed28d504ed9f7377a1437a86c0f4da:HotelSimulatie/HotelSimulatie/Classes/Areas/Reception.cs
        {
            this.AreaType = areaType;
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Width = width;
            this.Height = height;
            HotelEvents.HotelEventManager.Register(this);
        }

        public void Notify(HotelEvents.HotelEvent hotelEvent)
        {
            MessageBox.Show("GOT NOTIFICATION");
        }
    }
}
