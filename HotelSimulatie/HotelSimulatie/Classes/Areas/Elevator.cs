using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class Elevator : IArea
    {
        public EAreaType AreaType { get; set; } = EAreaType.Elevator;
        public int PositionX { get; set; } = 0;
        public int PositionY { get; set; }
        public int Width { get; set; } = 1;
        public int Height { get; set; } = 1;
        public Bitmap Sprite { get; set; } = Sprites.Elevator;

        public void Create(EAreaType areaType, int capacity, int classification, int positionX, int positionY, int width, int height)
        {
            this.AreaType = areaType;
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Width = width;
            this.Height = height;
        }
    }
}
