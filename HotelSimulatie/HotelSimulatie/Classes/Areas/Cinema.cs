using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class Cinema : IArea
    {
        public int ID { get; set; }
        public EAreaType AreaType { get; set; } = EAreaType.Cinema;

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int MovieTime { get; set; } = 12;

        public HashSet<Customer> WaitingLine { get; set; } = new HashSet<Customer>();

        public Bitmap Sprite { get; set; } = Sprites.Cinema;
        public Node Node { get; set; }

        public void Create(int ID, EAreaType areaType, int capacity,int classification, int positionX, int positionY, int width, int height)
        {
            this.ID = ID;
            this.AreaType = areaType;
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Width = width;
            this.Height = height;
            GlobalStatistics.Cinemas.Add(this);
        }
    }
}
