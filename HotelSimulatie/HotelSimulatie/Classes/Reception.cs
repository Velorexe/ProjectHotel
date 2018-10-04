using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class Reception : IArea, HotelEvents.HotelEventListener
    {
        public EAreaType AreaType { get; set; } = EAreaType.Reception;
        public int Height { get; set; } = 1;
        public int Width { get; set; } = 1;
        public int PositionX { get; set; } = 1;
        public int PositionY { get; set; }
<<<<<<< HEAD

=======
>>>>>>> e4b1564f7e66fbd0e5b941708e3afc0d40e4afd9
        public Bitmap Sprite { get; set; } = Sprites.Reception;

        public void Create(EAreaType areaType, int capacity, int classification, int positionX, int positionY, int width, int height, Bitmap sprite)
        {
            this.AreaType = areaType;
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Width = width;
            this.Height = height;
            this.Sprite = sprite;
        }

        public void Notify(HotelEvents.HotelEvent hotelEvent)
        {

        }
    }
}
