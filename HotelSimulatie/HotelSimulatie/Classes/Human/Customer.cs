﻿using System;
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
        public int PositionX { get; set; } = 1;
        public int PositionY { get; set; } = 0;
        public Bitmap Sprite { get; set; }

        public IHuman Create(string Name)
        {
            this.Name = Name;
            return this;
        }
    }
}
