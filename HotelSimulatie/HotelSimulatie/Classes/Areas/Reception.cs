using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using HotelEvents;

namespace HotelSimulatie
{
    class Reception : IArea, HotelEvents.HotelEventListener
    {
        public int ID { get; set; }
        public EAreaType AreaType { get; set; } = EAreaType.Reception;
        public int Height { get; set; } = 1;
        public int Width { get; set; } = 1;
        public int PositionX { get; set; } = 1;
        public int PositionY { get; set; }
        public Bitmap Sprite { get; set; } = Sprites.ReceptionBar;
        public Node Node { get; set; }
        private Queue<HotelEvent> CustomerQueue { get; set; } = new Queue<HotelEvent>();

        public void Create(int ID, EAreaType areaType, int capacity, int classification, int positionX, int positionY, int width, int height)
        {
            this.ID = ID;
            this.AreaType = areaType;
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Width = width;
            this.Height = height;
            HotelEventManager.Register(this);
        }

        public void HireCleaners(int CleanerAmount)
        {
            for (int i = 0; i < CleanerAmount; i++)
            {
                HumanFactory.CreateHuman(EHumanType.Cleaner);
                GlobalStatistics.Cleaners[i].CleanerID = i;
                GlobalStatistics.Cleaners[i].PositionX = PositionX;
                GlobalStatistics.Cleaners[i].PositionY = PositionY;
            }
        }

        public void GuestCheckIn()
        {
            if (CustomerQueue.Count > 0)
            {
                HotelEvent hotelEvent = CustomerQueue.Dequeue();
                List<Room> AvaiableRooms = new List<Room>();
                int Classification = PullIntsFromString(hotelEvent.Data.Values.First());

                Customer NewCustomer = (Customer)HumanFactory.CreateHuman(EHumanType.Customer);

                while (NewCustomer.AssignedRoom == null && Classification <= 5)
                {
                    for (int i = 0; i < GlobalStatistics.Rooms.Count; i++)
                    {
                        if (Classification == 0 && GlobalStatistics.Rooms[i].RoomOwner is null && GlobalStatistics.Rooms[i].IsDirty == false)
                        {
                            AvaiableRooms.Add(GlobalStatistics.Rooms[i]);
                        }
                        else if (GlobalStatistics.Rooms[i].Classification == Classification && GlobalStatistics.Rooms[i].RoomOwner is null && GlobalStatistics.Rooms[i].IsDirty == false)
                        {
                            AvaiableRooms.Add(GlobalStatistics.Rooms[i]);
                        }
                    }
                    if (AvaiableRooms.Count == 0)
                    {
                        Classification++;
                    }
                    else if (AvaiableRooms.Count == 1)
                    {
                        NewCustomer.AssignedRoom = AvaiableRooms[0];
                        AvaiableRooms[0].RoomOwner = NewCustomer;
                    }
                    else
                    {
                        NewCustomer.AssignedRoom = (Room)Graph.SearchNode(AvaiableRooms.OrderBy(x => x.PositionY).ThenBy(x => x.PositionX).ToList()[0]).Area;
                        NewCustomer.AssignedRoom.RoomOwner = NewCustomer;
                    }
                }

                if (NewCustomer.AssignedRoom != null)
                {
                    if (hotelEvent.Data != null && PullIntsFromString(hotelEvent.Data.Keys.First()) != 0)
                    {
                        NewCustomer.ID = PullIntsFromString(hotelEvent.Data.Keys.First());
                    }
                    else
                    {
                        NewCustomer.ID = 0;
                    }
                    NewCustomer.Destination = Graph.SearchNode(NewCustomer.AssignedRoom);
                    NewCustomer.MoveToLocation(this);
                }
            }
        }

        public void Notify(HotelEvents.HotelEvent hotelEvent)
        {
            if (hotelEvent.EventType == HotelEvents.HotelEventType.CHECK_IN)
            {
                CustomerQueue.Enqueue(hotelEvent);
            }
        }

        private int PullIntsFromString(string target)
        {
            int result = 0;
            target = target.Replace(" ", "");
            target = Regex.Replace(target, "[A-Za-z ]", "");
            string[] tempArray = target.Split(',');
            if(target == "")
            {
                return result;
            }
            for (int i = 0; i < tempArray.Length; i++)
            {
                result = Convert.ToInt32(tempArray[i]);
            }
            return result;
        }
    }
}
