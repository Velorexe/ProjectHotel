﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class Fitness : IArea
    {
        public EAreaType AreaType { get; set; } = EAreaType.Fitness;
        public int Capacity { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Height { get; set; } = 1;
        public int Width { get; set; } = 1;
        public Bitmap Sprite { get; set; } = HotelSimulatie.Properties.Resources.Gym;

        public void Create(EAreaType areaType, int capacity, int classification, int positionX, int positionY, int width, int height)
        {
            this.AreaType = areaType;
            this.Capacity = capacity;
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Width = width;
            this.Height = height;
        }
    }
}