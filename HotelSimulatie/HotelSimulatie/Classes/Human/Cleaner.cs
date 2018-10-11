using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class Cleaner : IHuman
    {
        public string Name { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Queue<Node> Path { get; set; } = new Queue<Node>();
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
