﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class Hallway : IArea
    {
        public int ID { get; set; }
        public EAreaType AreaType { get; set; } = EAreaType.Hallway;
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; } = 1;
        public int Height { get; set; } = 1;
        public Bitmap Sprite { get; set; } = Sprites.Room;
        public Node Node { get; set; }

        public void Create(int ID, EAreaType areaType, int capacity, int classification, int positionX, int positionY, int width, int height)
        {
            this.ID = ID;
            this.AreaType = areaType;
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Width = width;
            this.Height = height;
        }
    }
}
