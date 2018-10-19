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
    class Cleaner : IHuman, IMoveAble, HotelEventListener
    {
        //ID of the Cleaner
        public int CleanerID { get; set; } = 0;
        //All Cleaners are given a random generated name
        public string Name { get; set; }

        //PositionX is a point in the grid of the simulation (together with the PositionY it makes a position for the Cleaner)
        public int PositionX { get; set; }
        //PositionX is a point in the grid of the simulation (together with the PositionY it makes a position for the Cleaner)
        public int PositionY { get; set; }
        
        //The destination of the Cleaner
        public Node Destination { get; set; }

        //Tasks the Cleaners have to do
        public Queue<CleanRoom> CleanerTasks { get; set; } = new Queue<CleanRoom>();
        //The current task the Cleaner is assigned to 
        public CleanRoom CurrentTask { get; set; }

        //Is only being used for the most important events (like EVACUATE)
        private HotelEventType Status { get; set; } = HotelEventType.NONE;

        //A boolean to see if the Cleaner must be shown of screen or not
        public bool IsVisible { get; set; } = true;

        //The time a Cleaner has to wait before continuing an action
        public int WaitingTime { get; set; }

        //Check if Cleaner is in the Elevator
        public bool IsInElevator { get; set; }
        //Check if the Cleaner requested the elevator to its floor
        public bool RequestedElevator { get; set; }

        //Because of different threads we need to check if the Cleaner is registered of not.
        //If IsRegistered is true the Cleaner will be added to HotelEventManager
        //If IsRegistered is false nothing will be done to the Cleaner
        public bool IsRegistered { get; set; } = false;

        //The Route that is given to a Cleaner based on the quickest path to the destination
        public Route Path { get; set; }
        //A sprite is given to the Cleaner based on the HumanType
        public Bitmap Sprite { get; set; } = Sprites.Maid;

        public void MoveToLocation(IArea CurrentLocation)
        {

        }

        public void CleanRoom(CleanRoom RoomToClean)
        {
            CleanerTasks.Enqueue(RoomToClean);
        }

        public void Notify(HotelEvent Event)
        {
            if (Status != HotelEventType.EVACUATE)
            {
                if (Event.EventType == HotelEventType.EVACUATE)
                {
                    CurrentTask = null;
                    CleanerTasks.Clear();
                    Status = HotelEventType.EVACUATE;
                    Destination = Hotel.Reception.Node;
                    Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);
                }
                if (Event.EventType == HotelEventType.CLEANING_EMERGENCY)
                {
                    int[] Data = PullIntsFromString(Event.Data.Values.ToList());
                    for (int i = 0; i < GlobalStatistics.Rooms.Count; i++)
                    {
                        if (GlobalStatistics.Rooms[i].ID == Data[0])
                        {
                            CleanerTasks.Enqueue(new CleanRoom() { RoomToClean = GlobalStatistics.Rooms[i].Node, TimeToClean = Data[1] });
                        }
                    }
                }
            }
        }

        private void MoveToOptimalPosition()
        {
            Destination = Graph.SearchNode(Hotel.Floors[Hotel.Floors.Length / GlobalStatistics.Cleaners.Count * CleanerID].Areas[Hotel.Floors[0].Areas.Length / 2]);
            Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Graph.SearchNode(Hotel.Floors[Hotel.Floors.Length / GlobalStatistics.Cleaners.Count * CleanerID].Areas[Hotel.Floors[0].Areas.Length / 2]), true, true);
        }

        public void Move()
        {
            if (!IsRegistered)
            {
                HotelEventManager.Register(this);
                IsRegistered = true;
            }

            if (CurrentTask == null && CleanerTasks.Count > 0)
            {
                CurrentTask = CleanerTasks.Dequeue();
            }
            else if (CurrentTask == null && Destination == null && CleanerTasks.Count == 0)
            {
                MoveToOptimalPosition();
            }

            if (CurrentTask != null && (Destination == null || Destination == Hotel.Floors[PositionY].Areas[PositionX].Node))
            {
                Destination = CurrentTask.RoomToClean;
                Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);
            }

            if (CurrentTask != null && Hotel.Floors[PositionY].Areas[PositionX].Node == CurrentTask.RoomToClean)
            {
                if (((Room)CurrentTask.RoomToClean.Area).CleaningTime == 0 && ((Room)CurrentTask.RoomToClean.Area).IsDirty == true)
                {
                    ((Room)CurrentTask.RoomToClean.Area).CleaningTime = CurrentTask.TimeToClean;
                    IsVisible = false;
                }
                else
                {
                    if (((Room)CurrentTask.RoomToClean.Area).CleaningTime > 0)
                    {
                        ((Room)CurrentTask.RoomToClean.Area).CleaningTime--;
                        if (((Room)CurrentTask.RoomToClean.Area).CleaningTime == 0)
                        {
                            ((Room)CurrentTask.RoomToClean.Area).IsDirty = false;
                            IsVisible = true;
                            CurrentTask = null;
                            Destination = null;
                        }
                    }
                }
            }


            if (Path != null)
            {
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

            if (Destination != null)
            {
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
        }

        public IHuman Create(string Name)
        {
            GlobalStatistics.Cleaners.Add(this);
            this.Name = Name;
            return this;
        }

        private int[] PullIntsFromString(List<string> Data)
        {
            int[] result = new int[Data.Count];
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
                for (int i = 0; i < tempArray.Length; i++)
                {
                    result[i] = Convert.ToInt32(tempArray[i]);
                }
            }
            return result;
        }
    }
}
