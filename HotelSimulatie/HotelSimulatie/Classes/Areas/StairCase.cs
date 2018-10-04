using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class Staircase : IArea
    {
        public EAreaType AreaType { get; set; } = EAreaType.Staircase;
        public int Width { get; set; } = 1;
        public int Height { get; set; } = 1;
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Bitmap Sprite { get; set; } = Sprites.Staircase;

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
