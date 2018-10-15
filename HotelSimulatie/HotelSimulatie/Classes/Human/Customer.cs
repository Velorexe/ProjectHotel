using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class Customer : IHuman, IMoveAble
    {
        public string Name { get; set; }
        public int PositionX { get; set; } = 1;
        public int PositionY { get; set; } = 0;
        public Route Path { get; set; }
        public Node Destination { get; set; }
        public Room AssignedRoom { get; set; } = null;
        public Bitmap Sprite { get; set; } = Sprites.Customer;
        public bool IsInRoom { get; set; } = false;

        private bool IsInElevator { get; set; } = false;
        private bool ReqestedElevator { get; set; } = false;

        private int WaitingTime { get; set; } = 0;

        public void MoveToLocation(IArea CurrentLocation)
        {
            Path = Graph.QuickestRoute(Graph.SearchNode(CurrentLocation), Graph.SearchNode(Destination.Area), true, true);
        }

        public void Move()
        {

            #region Elevator
            if (Path.RouteType == ERouteType.ToElevator && Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft)
            {
                GetRoute();
            }
            else if (Path.RouteType == ERouteType.Elevator)
            {
                if (!IsInElevator)
                {
                    if (Hotel.Elevator.GetElevatorInfo().Item2 == PositionY && Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft && !IsInElevator)
                    {
                        Hotel.Elevator.RequestElevator(PositionY, Destination.Floor);
                        PositionX--;
                        IsInElevator = true;
                        ReqestedElevator = false;
                        Hotel.Elevator.InElevator.Add(this);
                    }
                    else if (Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft && !IsInElevator)
                    {
                        if (!ReqestedElevator)
                        {
                            Hotel.Elevator.RequestElevator(PositionY, Destination.Floor);
                            ReqestedElevator = true;
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
        }

        private void GetRoute()
        {
            if (Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft)
            {
                Tuple<char, int> ElevatorInfo = Hotel.Elevator.GetElevatorInfo().ToTuple();
                int ElevatorTime = 0;

                if (ElevatorInfo.Item1 == 'D' && ElevatorInfo.Item2 < PositionY)
                {
                    ElevatorTime += ElevatorInfo.Item2;
                    ElevatorTime += PositionY;
                }
                else if (ElevatorInfo.Item1 == 'D' && ElevatorInfo.Item2 > PositionY)
                {
                    ElevatorTime += ElevatorInfo.Item2 - PositionY;
                }
                else if (ElevatorInfo.Item1 == 'U' && ElevatorInfo.Item2 < PositionY)
                {
                    ElevatorTime += PositionY - ElevatorInfo.Item2;
                }
                else if (ElevatorInfo.Item1 == 'U' && ElevatorInfo.Item2 > PositionY)
                {
                    ElevatorTime += Hotel.Floors.Length - ElevatorInfo.Item2;
                    ElevatorTime += Hotel.Floors.Length - PositionY;
                }

                ElevatorTime += Math.Abs(ElevatorInfo.Item2 - PositionY);

                if (Path.PathToElevatorLength + Path.PathFromElevatorLength + ElevatorTime < Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, false, true).PathLength)
                {
                    Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, false);
                    Path.RouteType = ERouteType.Elevator;
                }
                else
                {
                    Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, false, true);
                    Path.RouteType = ERouteType.Stairs;
                }
            }
        }

        public IHuman Create(string Name)
        {
            this.Name = Name;
            return this;
        }
    }
}
