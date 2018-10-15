using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class Cleaner : IHuman, IMoveAble
    {
        public string Name { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Route Path { get; set; }
        public Bitmap Sprite { get; set; } = Sprites.Maid;

        public void Move()
        {

        }
        public IHuman Create(string Name)
        {
            this.Name = Name;
            return this;
        }
    }
}
