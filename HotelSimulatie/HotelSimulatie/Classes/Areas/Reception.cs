using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace HotelSimulatie
{
    class Reception : IArea, HotelEvents.HotelEventListener
    {
        public EAreaType AreaType { get; set; } = EAreaType.Reception;
        public int Height { get; set; } = 1;
        public int Width { get; set; } = 1;
        public int PositionX { get; set; } = 1;
        public int PositionY { get; set; }
        public Bitmap Sprite { get; set; } = Sprites.ReceptionBar;

        public List<Room> Rooms = new List<Room>();
        public List<IHuman> Customers = new List<IHuman>();
        public List<IHuman> Cleaners = new List<IHuman>();

        public void Create(EAreaType areaType, int capacity, int classification, int positionX, int positionY, int width, int height)
        {
            this.AreaType = areaType;
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Width = width;
            this.Height = height;
            HotelEvents.HotelEventManager.Register(this);
        }

        public void AddAllRooms(Hotel hotel)
        {
            for (int i = 0; i < hotel.Floors.Length; i++)
            {
                for (int j = 0; j < hotel.Floors[i].Areas.Length; j++)
                {
                    if(hotel.Floors[i].Areas[j].AreaType == EAreaType.Room)
                    {
                        this.Rooms.Add((Room)hotel.Floors[i].Areas[j]);
                    }
                }
            }
        }

        public void Notify(HotelEvents.HotelEvent hotelEvent)
        {
            if(hotelEvent.EventType == HotelEvents.HotelEventType.CHECK_IN)
            {
                List<Room> AvaiableRooms = new List<Room>();
                int Classification = PullIntsFromString(hotelEvent.Message);

                Customer NewCustomer = (Customer)HumanFactory.CreateHuman(EHumanType.Customer);

                for (int i = 0; i < Rooms.Count; i++)
                {
                    if(Classification == 0 && Rooms[i].RoomOwner is null)
                    {
                        AvaiableRooms.Add(Rooms[i]);
                    }
                    else if(Rooms[i].Classification == Classification && Rooms[i].RoomOwner is null)
                    {
                        AvaiableRooms.Add(Rooms[i]);
                    }
                }

                Random r = new Random();
                int RandomNumber;
                for (int i = 0; i < AvaiableRooms.Count; i++)
                {
                    RandomNumber = r.Next(0, AvaiableRooms.Count - 1);
                    NewCustomer.AssignedRoom = AvaiableRooms[RandomNumber];
                    AvaiableRooms[RandomNumber].RoomOwner = NewCustomer;
                }

                if (NewCustomer.AssignedRoom != null)
                    this.Customers.Add(NewCustomer);
            }
        }

        private int PullIntsFromString(string target)
        {
            int result = 0;
            target = target.Replace(" ", "");
            target = Regex.Replace(target, "[A-Za-z ]", "");
            string[] tempArray = target.Split(',');
            for (int i = 0; i < tempArray.Length; i++)
            {
                result = Convert.ToInt32(tempArray[i]);
            }
            return result;
        }
    }
}
