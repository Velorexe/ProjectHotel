using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    public class Restaurant : IArea
    {
        public EAreaType AreaType { get; set; } = EAreaType.Restaurant;
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Capacity { get; set; }
        public Bitmap Sprite { get; set; } = Sprites.Restaurant;
    }
}
