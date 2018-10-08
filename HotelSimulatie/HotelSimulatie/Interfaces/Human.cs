﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    interface IHuman
    {
        string Name { get; set; }
        Bitmap Sprite { get; set; }

        void Create(string Name);
    }
}