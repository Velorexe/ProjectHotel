﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    public class Cinema : IArea
    {
        public EAreaType AreaType { get; set; } = EAreaType.Cinema;
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Capacity { get; set; } = 10;
        public Bitmap Sprite { get; set; } = Sprites.Cinema;

        public void Create(EAreaType areaType, int capacity,int classification, int positionX, int positionY, int width, int height, Bitmap sprite)
        {
            this.AreaType = areaType;
            this.Capacity = capacity;
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Width = width;
            this.Height = height;
            this.Sprite = sprite;
        }
    }
}
