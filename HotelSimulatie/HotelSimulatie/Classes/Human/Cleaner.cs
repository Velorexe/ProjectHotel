using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    public class Cleaner : IHuman
    {
        public string Name { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Bitmap Sprite { get; set; } // = Sprites.Cleaner
        public IHuman Create(string Name)
        {
            this.Name = Name;
            return this;
        }
    }
}
