﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    public class Room : IArea
    {
        public EAreaType AreaType { get; set; } = EAreaType.Room;
        public int Classification { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int Capacity { get; set; } = 1;
    }
}