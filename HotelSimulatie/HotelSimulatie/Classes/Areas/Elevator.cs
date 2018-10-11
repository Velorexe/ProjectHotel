using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class Elevator
    {
        public EAreaType AreaType { get; set; } = EAreaType.Elevator;
        public int PositionX { get; set; } = 0;
        public int PositionY { get; set; }
        public int Width { get; set; } = 1;
        public int Height { get; set; } = 1;
        public Bitmap Sprite { get; set; } = Sprites.Elevator;

        //'U' for UP, 'D' for DOWN
        public char Direction { get; set; } = 'U';
    }
}
