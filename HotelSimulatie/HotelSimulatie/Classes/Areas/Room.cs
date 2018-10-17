using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class Room : IArea
    {
        public int ID { get; set; }
        public EAreaType AreaType { get; set; } = EAreaType.Room;
        public int Classification { get; set; }

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Capacity { get; set; } = 1;

        public bool IsDirty { get; set; } = false;
        public int CleaningTime { get; set; }

        public Customer RoomOwner { get; set; }
        public Bitmap Sprite { get; set; } = Sprites.RoomDoor;
        public Bitmap Occupied { get; set; } = Sprites.Occupied;
        public Node Node { get; set; }

        public void Dirty()
        {
            int CleanerTasks = 0;
            int CleanerID = 0;

            for (int i = 0; i < GlobalStatistics.Cleaners.Count; i++)
            {
                if(GlobalStatistics.Cleaners[i].roomsToClean.Count > CleanerTasks)
                {
                    CleanerTasks = GlobalStatistics.Cleaners[i].roomsToClean.Count;
                    CleanerID = i;
                }
            }

            GlobalStatistics.Cleaners[CleanerID].roomsToClean.Enqueue(new CleanRoom() { RoomToClean = Node, TimeToClean = Hotel.Settings.CleaningTime });
        }

        public void Create(int ID, EAreaType areaType, int capacity, int classification, int positionX, int positionY, int width, int height)
        {
            this.ID = ID;
            this.AreaType = areaType;
            this.Classification = classification;
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Width = width;
            this.Height = height;
            GlobalStatistics.Rooms.Add(this);
        }
    }
}
