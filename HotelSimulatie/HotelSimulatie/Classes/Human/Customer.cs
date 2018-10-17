using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using HotelEvents;
using System.Text.RegularExpressions;

namespace HotelSimulatie
{
    class Customer : IHuman, IMoveAble, HotelEventListener
    {
        public int ID { get; set; } = 0;
        public string Name { get; set; }

        public bool IsVisible { get; set; } = true;

        public int PositionX { get; set; } = 1;
        public int PositionY { get; set; } = 0;

        public bool IsRegistered { get; set; } = false;

        public HotelEventType Status { get; set; } = HotelEventType.NONE;
        public ECustomerStatus CustomerStatus { get; set; } = ECustomerStatus.IDLE;

        public Route Path { get; set; }
        public Node Destination { get; set; }
        public Room AssignedRoom { get; set; } = null;
        public Bitmap Sprite { get; set; } = Sprites.Customer;

        private bool IsInElevator { get; set; } = false;
        private bool RequestedElevator { get; set; } = false;

        private int WaitingTime { get; set; } = 0;

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

            if(Destination != null && AssignedRoom == Destination.Area)
            {
                CustomerStatus = ECustomerStatus.GOING_TO_ROOM;
            }

            #region Elevator
            if (Path.RouteType == ERouteType.ToElevator && Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft)
            {
                GetRoute();
            }
            else if(Path.RouteType == ERouteType.ToElevator && Path.PathToElevator.Count != 0)
            {
                Node moveNode = Path.PathToElevator.Dequeue();
                this.PositionX = moveNode.Area.PositionX;
                this.PositionY = moveNode.Area.PositionY;
            }
            if (Path.RouteType == ERouteType.Elevator)
            {
                if (!IsInElevator)
                {
                    if (Hotel.Elevator.GetElevatorInfo().Item2 == PositionY && Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft && !IsInElevator)
                    {
                        Hotel.Elevator.RequestElevator(PositionY, Destination.Floor);
                        PositionX--;
                        IsInElevator = true;
                        RequestedElevator = false;
                        Hotel.Elevator.InElevator.Add(this);
                    }
                    else if (Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft && !IsInElevator)
                    {
                        if (!RequestedElevator)
                        {
                            Hotel.Elevator.RequestElevator(PositionY, Destination.Floor);
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
                if (WaitingTime > 0)
                {
                    WaitingTime--;
                }
                else
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
            }
            #endregion

            if(CustomerStatus != ECustomerStatus.IN_ROOM || Hotel.Floors[PositionY].Areas[PositionX] != AssignedRoom)
            {
                IsVisible = true;
            }
            if(Hotel.Floors[PositionY].Areas[PositionX].Node == Destination && CustomerStatus == ECustomerStatus.GOING_TO_ROOM)
            {
                CustomerStatus = ECustomerStatus.IN_ROOM;
                IsVisible = false;
                Destination = null;
            }
            else if(Hotel.Floors[PositionY].Areas[PositionX].Node == Destination)
            {
                IsVisible = true;
                Destination = null;
            }

            if(Status == HotelEventType.CHECK_OUT && Hotel.Floors[PositionY].Areas[PositionX] == Hotel.Reception)
            {
                GlobalStatistics.Customers.Remove(this);
                HotelEventManager.Deregister(this);
            }
        }

        private void GetRoute()
        {
            Tuple<char, int> ElevatorInfo = Hotel.Elevator.GetElevatorInfo().ToTuple();
            int ElevatorTime = 0;

            if(ElevatorInfo.Item1 == 'I')
            {
                if(ElevatorInfo.Item2 < PositionY)
                {
                    ElevatorTime += PositionY - ElevatorInfo.Item2;
                }
                else
                {
                    ElevatorTime += ElevatorInfo.Item2 - PositionY;
                }
            }
            else if(ElevatorInfo.Item1 == 'U')
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
            else if(ElevatorInfo.Item1 == 'D')
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
            if(Event.EventType == HotelEventType.CHECK_OUT)
             {
                if(Event.Data.Keys.First() == "Gast")
                {
                    int[] Data = PullIntsFromString(Event.Data.Values.ToList());
                    if(ID == Data[0])
                    {
                        AssignedRoom.Dirty();
                        AssignedRoom.RoomOwner = null;
                        Destination = Hotel.Reception.Node;
                        Path = Graph.QuickestRoute(Graph.SearchNode(Hotel.Floors[PositionY].Areas[PositionX]), Destination, true, true);
                        Status = HotelEventType.CHECK_OUT;
                    }
                }
            }
            else if(Event.EventType == HotelEventType.NEED_FOOD)
            {
                if(Event.Data.Keys.First() == "Gast")
                {
                    int[] Data = PullIntsFromString(Event.Data.Values.ToList());
                    if(ID == Data[0])
                    {
                        Status = HotelEventType.NEED_FOOD;
                        Destination = Graph.NearestFacility(Hotel.Floors[PositionY].Areas[PositionX].Node, EAreaType.Restaurant);
                        Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);
                    }
                }
            }
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
        public enum ECustomerStatus
        {
            IN_ROOM,
            IDLE,
            GOING_TO_ROOM
        }
    }
}
