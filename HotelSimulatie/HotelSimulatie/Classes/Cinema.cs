using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    public class Cinema : IArea
    {
        public EAreaType AreaType { get; set; } = EAreaType.Cinema;
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Capacity { get; set; } = 10;
        public Bitmap Sprite { get; set; } = Sprites.Cinema;
    }
}
