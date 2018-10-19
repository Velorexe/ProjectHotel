using HotelEvents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace HotelSimulatie
{
    class Customer : IHuman, IMoveAble, HotelEventListener
    {
        public int ID { get; set; } = 0;
        //All Customers are given a random generated name 
        public string Name { get; set; }

        //A boolean to see if the Customer must be shown of screen or not
        public bool IsVisible { get; set; } = true;

        //PositionX is a point in the grid of the simulation (together with the PositionY it makes a position for the Customer)
        public int PositionX { get; set; } = 1;
        //PositionX is a point in the grid of the simulation (together with the PositionY it makes a position for the Customer)
        public int PositionY { get; set; } = 0;

        //Because of different threads we need to check if the Customer is registered of not.
        //If IsRegistered is true the Customer will be added to HotelEventManager
        //If IsRegistered is false nothing will be done to the Customer
        public bool IsRegistered { get; set; } = false;

        //A check if the Customer is waitng 
        private bool IsWaiting { get; set; } = false;
        //The time that needs to go by before a Customer dies
        private int DeathTimer { get; set; } = 0;
        //LastLocation is a check for Customer to see if the Customer is in the same location as the last check and if the Customer is not in an Area
        private Node LastLocation { get; set; }

        //The time it takes for the customer to finish fitnessing, is assigned when GOTO_FITNESS event is called
        private int FitnessTime { get; set; } = 0;

        //The status of the Customer (mostly used for important events like EVACUATE)
        public HotelEventType Status { get; set; } = HotelEventType.NONE;

        //The Area that the Customer is in
        public IArea InArea { get; set; }

        //The Route that is given to a Customer based on the quickest path to the destination
        public Route Path { get; set; }
        //The destination of the Customer
        public Node Destination { get; set; }
        //The Room that is assigned to the Customer
        public Room AssignedRoom { get; set; } = null;
        //A sprite is given to the Customer based on the HumanType
        public Bitmap Sprite { get; set; } = Sprites.Customer;

        //Check if Customer is in the Elevator
        private bool IsInElevator { get; set; } = false;
        //Check if the Customer requested the elevator to its floor
        private bool RequestedElevator { get; set; } = false;

        //The time a Customer has to wait before continuing an action
        private int WaitingTime { get; set; } = 0;

        /// <summary>
        /// The Customer gets a Path to certain location
        /// </summary>
        /// <param name="CurrentLocation">The Location from where the Customer must calculate the Quickest Route</param>
        public void MoveToLocation(IArea CurrentLocation)
        {
            Path = Graph.QuickestRoute(Graph.SearchNode(CurrentLocation), Graph.SearchNode(Destination.Area), true, true);
        }

        public void Move()
        {

            if (!IsRegistered)
            {
                HotelEventManager.Register(this);
                IsRegistered = true;
            }

            if (IsWaiting == true && DeathTimer >= Hotel.Settings.TimeBeforeDeath)
            {
                GlobalStatistics.Customers.Remove(this);
                HotelEventManager.Deregister(this);
            }

            if (WaitingTime > 0)
            {
                WaitingTime--;
            }
            else if (WaitingTime == 0)
            {
                if (Path != null)
                {
                    #region Elevator
                    if (Path.RouteType == ERouteType.ToElevator && Path.PathToElevator.Count != 0)
                    {
                        Node moveNode = Path.PathToElevator.Dequeue();
                        if (moveNode.NodeType != ENodeType.Elevatorshaft)
                        {
                            PositionX = moveNode.Area.PositionX;
                            PositionY = moveNode.Area.PositionY;
                        }
                        if (Path.PathToElevator.Count == 0)
                        {
                            GetRoute();
                        }
                    }
                    if (Path.RouteType == ERouteType.Elevator)
                    {
                        if (!IsInElevator)
                        {
                            if (Hotel.Elevator.GetElevatorInfo().Item2 == PositionY && Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft && !IsInElevator)
                            {
                                Hotel.Elevator.RequestElevator(Destination.Floor);
                                PositionX--;
                                IsInElevator = true;
                                RequestedElevator = false;
                                Hotel.Elevator.InElevator.Add(this);
                            }
                            else if (Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft && !IsInElevator)
                            {
                                if (!RequestedElevator)
                                {
                                    Hotel.Elevator.RequestElevator(PositionY);
                                    RequestedElevator = true;
                                }
                            }
                        }
                        else
                        {
                            if (PositionY == Destination.Floor)
                            {
                                Node moveNode = Path.PathFromElevator.Dequeue();
                                this.PositionX = moveNode.Area.PositionX;
                                this.PositionY = moveNode.Area.PositionY;

                                Hotel.Elevator.InElevator.Remove(this);
                                Path.RouteType = ERouteType.FromElevator;

                                IsInElevator = false;
                            }
                        }
                    }
                    else if (Path.RouteType == ERouteType.FromElevator && Path.PathFromElevator.Count != 0)
                    {
                        Node moveNode = Path.PathFromElevator.Dequeue();
                        this.PositionX = moveNode.Area.PositionX;
                        this.PositionY = moveNode.Area.PositionY;

                    }
                    #endregion

                    #region Stairs
                    if (Path.RouteType == ERouteType.Stairs)
                    {
                        if (Path.Path.Count != 0)
                        {
                            Node moveNode = Path.Path.Dequeue();
                            this.PositionX = moveNode.Area.PositionX;
                            this.PositionY = moveNode.Area.PositionY;
                            if (moveNode.Area.AreaType == EAreaType.Staircase)
                            {
                                WaitingTime = WaitingTime + Hotel.Settings.StairCase - 1;
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, AssignedRoom.Node, true, true);
                }

                if (Destination == null)
                {
                    Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, AssignedRoom.Node, true, true);
                }
            }

            if (InArea == null)
            {
                IsVisible = true;
            }
            else
            {
                IsVisible = false;
            }

            if (Hotel.Floors[PositionY].Areas[PositionX].Node == Destination)
            {
                if (Destination.Area.AreaType == EAreaType.Restaurant)
                {
                    WaitingTime = ((Restaurant)Destination.Area).EatingTime;
                    InArea = Destination.Area;
                    Destination = AssignedRoom.Node;
                    Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);
                }
                else if (Destination.Area.AreaType == EAreaType.Cinema)
                {
                    if (!((Cinema)Destination.Area).MovieStarted)
                    {
                        ((Cinema)Destination.Area).WaitingLine.Add(this);
                        IsWaiting = true;
                    }
                    else
                    {
                        Destination = AssignedRoom.Node;
                        Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);
                    }
                }
                else if(Destination.Area.AreaType == EAreaType.Fitness)
                {
                    WaitingTime = FitnessTime;
                    InArea = Destination.Area;
                    Destination = AssignedRoom.Node;
                    Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);
                }
                else if (Hotel.Floors[PositionY].Areas[PositionX] == AssignedRoom)
                {
                    InArea = AssignedRoom;
                }
            }
            else if(WaitingTime == 0)
            {
                InArea = null;
            }

            if (Status == HotelEventType.CHECK_OUT && Hotel.Floors[PositionY].Areas[PositionX] == Hotel.Reception)
            {
                GlobalStatistics.Customers.Remove(this);
                HotelEventManager.Deregister(this);
            }

            if (LastLocation == Hotel.Floors[PositionY].Areas[PositionX].Node && InArea == null)
            {
                IsWaiting = true;
                DeathTimer++;
            }
            else
            {
                IsWaiting = false;
                DeathTimer = 0;
            }
            LastLocation = Hotel.Floors[PositionY].Areas[PositionX].Node;
        }

        private void GetRoute()
        {
            Tuple<ElevatorDirection, int> ElevatorInfo = Hotel.Elevator.GetElevatorInfo().ToTuple();
            int ElevatorTime = 0;

            if (ElevatorInfo.Item1 == ElevatorDirection.IDLE)
            {
                if (ElevatorInfo.Item2 < PositionY)
                {
                    ElevatorTime += PositionY - ElevatorInfo.Item2;
                }
                else
                {
                    ElevatorTime += ElevatorInfo.Item2 - PositionY;
                }
            }
            else if (ElevatorInfo.Item1 == ElevatorDirection.UP)
            {
                if (ElevatorInfo.Item2 < PositionY)
                {
                    ElevatorTime += PositionY - ElevatorInfo.Item2;
                }
                else if (ElevatorInfo.Item2 > PositionY)
                {
                    ElevatorTime += Hotel.Floors.Length - ElevatorInfo.Item2;
                    ElevatorTime += Hotel.Floors.Length - PositionY;
                }
            }
            else if (ElevatorInfo.Item1 == ElevatorDirection.DOWN)
            {
                if (ElevatorInfo.Item2 < PositionY)
                {
                    ElevatorTime += ElevatorInfo.Item2;
                    ElevatorTime += PositionY;
                }
                else if (ElevatorInfo.Item2 > PositionY)
                {
                    ElevatorTime += ElevatorInfo.Item2 - PositionY;
                }
            }

            if (Path.PathToElevatorLength + Path.PathFromElevatorLength + ElevatorTime < Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, false, true).PathLength)
            {
                Path = Graph.QuickestRoute(Graph.SearchNode(Hotel.Floors[PositionY].Areas[PositionX]), Destination, true, false);
                Path.RouteType = ERouteType.Elevator;
            }
            else
            {
                Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, false, true);
                Path.RouteType = ERouteType.Stairs;
            }
        }

        public IHuman Create(string Name)
        {
            GlobalStatistics.Customers.Add(this);
            this.Name = Name;
            return this;
        }

        public void Notify(HotelEvent Event)
        {
            if (Status != HotelEventType.EVACUATE)
            {
                if(Event.EventType == HotelEventType.EVACUATE)
                {
                    Status = HotelEventType.EVACUATE;
                    Destination = Hotel.Reception.Node;
                    Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);
                }
                else if(Event.EventType == HotelEventType.GOTO_FITNESS)
                {
                    if (Event.Data.Keys.First() == "Gast")
                    {
                        int[] Data = PullIntsFromString(Event.Data.Values.ToList());
                        if (ID == Data[0])
                        {
                            //FINISH THIS LOL
                            FitnessTime = Data[1];
                            Destination = Graph.NearestFacility(Hotel.Floors[PositionY].Areas[PositionX].Node, EAreaType.Fitness);
                            Path = Graph.QuickestRoute(Graph.SearchNode(Hotel.Floors[PositionY].Areas[PositionX]), Destination, true, true);
                            Status = HotelEventType.GOTO_FITNESS;
                        }
                    }
                }
                else if (Event.EventType == HotelEventType.CHECK_OUT)
                {
                    if (Event.Data.Keys.First() == "Gast")
                    {
                        int[] Data = PullIntsFromString(Event.Data.Values.ToList());
                        if (ID == Data[0])
                        {
                            AssignedRoom.Dirty();
                            AssignedRoom.RoomOwner = null;
                            Destination = Hotel.Reception.Node;
                            Path = Graph.QuickestRoute(Graph.SearchNode(Hotel.Floors[PositionY].Areas[PositionX]), Destination, true, true);
                            Status = HotelEventType.CHECK_OUT;
                        }
                    }
                }
                else if (Event.EventType == HotelEventType.GOTO_CINEMA)
                {
                    if (Event.Data.Keys.First() == "Gast")
                    {
                        int[] Data = PullIntsFromString(Event.Data.Values.ToList());
                        if (ID == Data[0])
                        {
                            Status = HotelEventType.GOTO_CINEMA;
                            Destination = Graph.NearestFacility(Hotel.Floors[PositionY].Areas[PositionX].Node, EAreaType.Cinema);
                            Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);
                        }
                    }
                }
                else if (Event.EventType == HotelEventType.NEED_FOOD)
                {
                    if (Event.Data.Keys.First() == "Gast")
                    {
                        int[] Data = PullIntsFromString(Event.Data.Values.ToList());
                        if (ID == Data[0])
                        {
                            Status = HotelEventType.NEED_FOOD;
                            Destination = Graph.NearestFacility(Hotel.Floors[PositionY].Areas[PositionX].Node, EAreaType.Restaurant);
                            Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);
                        }
                    }
                }
            }
        }

        public void InCinema(int MovieTime, Cinema Area)
        {
            WaitingTime = MovieTime;
            InArea = Area;
            IsWaiting = false;
        }

        private int[] PullIntsFromString(List<string> Data)
        {
            int[] result = new int[0];
            for (int j = 0; j < Data.Count; j++)
            {
                string target = Data[j];
                if (target is null)
                {
                    return new int[] { 0, 0 };
                }
                target = target.Replace(" ", "");
                target = Regex.Replace(target, "[A-Za-z ]", "");
                string[] tempArray = target.Split(',');
                result = new int[tempArray.Length];
                for (int i = 0; i < tempArray.Length; i++)
                {
                    result[i] = Convert.ToInt32(tempArray[i]);
                }
            }
            return result;
        }
    }
}
