using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class Customer : IHuman
    {
        public string Name { get; set; }
        public Bitmap Sprite { get; set; }

        public void Create(string Name)
        {
            this.Name = Name;
        }
    }
}
