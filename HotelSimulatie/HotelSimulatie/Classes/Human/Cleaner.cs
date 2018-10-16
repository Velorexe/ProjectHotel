using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using HotelEvents;

namespace HotelSimulatie
{
    class Cleaner : IHuman, IMoveAble, HotelEventListener
    {
        public string Name { get; set; }

        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public int WaitingTime { get; set; }
        public Node Destination { get; set; }

        public bool IsInElevator { get; set; }
        public bool RequestedElevator { get; set; }

        public bool IsRegistered { get; set; } = false;

        public Route Path { get; set; }
        public Bitmap Sprite { get; set; } = Sprites.Maid;

        public void Notify(HotelEvent Event)
        {

        }

        private void MoveToOptimalPosition()
        {

        }

        public void Move()
        {
            if (!IsRegistered)
            {
                HotelEventManager.Register(this);
                IsRegistered = true;
            }

            #region Elevator
            if (Path.RouteType == ERouteType.ToElevator && Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft)
            {
                GetRoute();
            }
            else if (Path.RouteType == ERouteType.ToElevator && Path.PathToElevator.Count != 0)
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
        }

        private void GetRoute()
        {
            Tuple<char, int> ElevatorInfo = Hotel.Elevator.GetElevatorInfo().ToTuple();
            int ElevatorTime = 0;

            if (ElevatorInfo.Item1 == 'I')
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
            else if (ElevatorInfo.Item1 == 'U')
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
            else if (ElevatorInfo.Item1 == 'D')
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
            GlobalStatistics.Cleaners.Add(this);
            this.Name = Name;
            return this;
        }
    }
}
