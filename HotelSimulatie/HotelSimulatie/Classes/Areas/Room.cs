using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    public class Room : IArea
    {
        //Areas are given an ID 
        public int ID { get; set; }
        //Areas are given a AreaType based on what is given in the Lay-out file
        public EAreaType AreaType { get; set; } = EAreaType.Room;
        public int Classification { get; set; }

        //PositionX is a horizontal point in the grid of the simulation (Together with the PositionY it makes a location for the Area)
        public int PositionX { get; set; }
        //PositionY is a vertical point in the grid of the simulation (Together with the PositionX it makes a location for the Area)
        public int PositionY { get; set; }
        //Width of the Area
        public int Width { get; set; }
        //Height of the Area
        public int Height { get; set; }
        //How many Humans can be in the Area at the same time
        public int Capacity { get; set; } = 1;

        //If a room is dirty or not
        public bool IsDirty { get; set; } = false;
        //How much time it takes the Cleaners to clean the room
        public int CleaningTime { get; set; }

        //The assigned Customer to the Room
        public Customer RoomOwner { get; set; }
        //Areas have different sprites based on the AreaType
        public Bitmap Sprite { get; set; } = Sprites.RoomDoor;
        //Sprite if the room is occupied by a Customer
        public Bitmap Occupied { get; set; } = Sprites.Occupied;
        //Node given to the Area
        public Node Node { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void Dirty()
        {
            int CleanerTasks = GlobalStatistics.Cleaners[0].CleanerTasks.Count;
            int Cleaner = 0;

            for (int i = 0; i < GlobalStatistics.Cleaners.Count; i++)
            {
                if(GlobalStatistics.Cleaners[i].CleanerTasks.Count < CleanerTasks)
                {
                    Cleaner = i;
                    CleanerTasks = GlobalStatistics.Cleaners.Count;
                }
            }

            IsDirty = true;
            GlobalStatistics.Cleaners[Cleaner].CleanRoom(new CleanRoom() { RoomToClean = Node, TimeToClean = Hotel.Settings.CleaningTime });
        }

        /// <summary>
        /// Creation of an Area
        /// </summary>
        /// <param name="ID">ID of the Area</param>
        /// <param name="areaType">Type of Area</param>
        /// <param name="capacity">How many Humans can be in the Area at the same time</param>
        /// <param name="classification">The Classification of the Area</param>
        /// <param name="positionX">The horizontal point in the grid</param>
        /// <param name="positionY">The vertical point in the grid</param>
        /// <param name="width">The width of the Area</param>
        /// <param name="height">The height of the Area</param>
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
