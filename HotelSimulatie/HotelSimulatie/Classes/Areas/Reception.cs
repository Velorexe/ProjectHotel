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
    class Reception : IArea, HotelEventListener
    {
        //Areas are given an ID 
        public int ID { get; set; }
        //Areas are given a AreaType based on what is given in the Lay-out file
        public EAreaType AreaType { get; set; } = EAreaType.Reception;

        //Height of the Area
        public int Height { get; set; } = 1;
        //Width of the Area
        public int Width { get; set; } = 1;

        //PositionX is a horizontal point in the grid of the simulation (Together with the PositionY it makes a location for the Area)
        public int PositionX { get; set; } = 1;
        //PositionY is a vertical point in the grid of the simulation (Together with the PositionX it makes a location for the Area)
        public int PositionY { get; set; }

        //Areas have different sprites based on the AreaType
        public Bitmap Sprite { get; set; } = Sprites.ReceptionBar;

        //Node given to the Area
        public Node Node { get; set; }

        //A Queue that CHECK_IN HotelEvents will be placed in
        //Is read out every HTE to see if Customers need to be checked in or not
        private Queue<HotelEvent> CustomerQueue { get; set; } = new Queue<HotelEvent>();

        /// <summary>
        /// Creates an instance of Reception with the given Parameters
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
            AreaType = areaType;
            PositionX = positionX;
            PositionY = positionY;
            Width = width;
            Height = height;

            HotelEventManager.Register(this);
        }

        /// <summary>
        /// Creates a set amount of Cleaners and places them in the Hotel
        /// </summary>
        /// <param name="CleanerAmount">The amount of Cleaners that need to be placed into the Hotel</param>
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

        /// <summary>
        /// Assigns a Room to a Customer if their in the CustomerQueue. Checks if there's a room that fits with the Customer and assigns it to the Customer.
        /// </summary>
        public void GuestCheckIn()
        {
            if (CustomerQueue.Count > 0)
            {
                //Get's the HotelEvent from the Queue
                HotelEvent hotelEvent = CustomerQueue.Dequeue();
                //Creates a temporary List that saves the free rooms that fits the Customer's specifications
                List<Room> AvaiableRooms = new List<Room>();
                //Get's the classification that the Customer wants (saved into the HotelEvent.Data Dictionairy)
                int Classification = PullIntsFromString(hotelEvent.Data.Values.First());

                //The new Customer is created here
                Customer NewCustomer = (Customer)HumanFactory.CreateHuman(EHumanType.Customer);

                //While loop continues to check if there's a room free for the customer with the set Classification (or higher, but lower than 6)
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
                        //If there's no room with the given classification, we're going to check for a higher star room
                        Classification++;
                    }
                    else if (AvaiableRooms.Count == 1)
                    {
                        //If there's only one room avaiable, this rooms will directly be given to the Customer
                        NewCustomer.AssignedRoom = AvaiableRooms[0];
                        AvaiableRooms[0].RoomOwner = NewCustomer;
                    }
                    else
                    {
                        //If there's more than one room avaiable for the Customer, this function will look for the nearest room
                        NewCustomer.AssignedRoom = (Room)Graph.SearchNode(AvaiableRooms.OrderBy(x => x.PositionY).ThenBy(x => x.PositionX).ToList()[0]).Area;
                        NewCustomer.AssignedRoom.RoomOwner = NewCustomer;
                    }
                }

                //If the Customer has a room, an ID will be given to the Customer (provided in the HotelEvent)
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
                    //When everything is handled, the customer will be send to his room
                    NewCustomer.Destination = Graph.SearchNode(NewCustomer.AssignedRoom);
                    NewCustomer.MoveToLocation(this);
                }
            }
        }

        /// <summary>
        /// An event that's called everytime the HotelEventManager pushes out an HotelEvent
        /// </summary>
        /// <param name="hotelEvent">The HotelEvent containing information</param>
        public void Notify(HotelEvents.HotelEvent hotelEvent)
        {
            if (hotelEvent.EventType == HotelEvents.HotelEventType.CHECK_IN)
            {
                CustomerQueue.Enqueue(hotelEvent);
            }
        }

        /// <summary>
        /// A function that removes all the letters from a string and returns the int that's int the string.
        /// </summary>
        /// <param name="target">The string where the int needs to be pulled from.</param>
        /// <returns>The last int in the given string (if there's none, it will return 0).</returns>
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
