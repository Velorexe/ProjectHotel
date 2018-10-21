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
    public class Cleaner : IHuman, IMoveAble, HotelEventListener
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
        //The current task the Cleaner is assigned to do
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

        /// <summary>
        /// Enqueues a new Task for the Cleaner to do.
        /// </summary>
        /// <param name="RoomToClean">The CleanRoom task that the Cleaner needs to fullfill.</param>
        public void CleanRoom(CleanRoom RoomToClean)
        {
            CleanerTasks.Enqueue(RoomToClean);
        }

        /// <summary>
        /// An event that's called everytime the HotelEventManager pushes out an HotelEvent.
        /// </summary>
        /// <param name="Event">The HotelEvent containing event information.</param>
        public void Notify(HotelEvent Event)
        {
            //If the Cleaner is evacuating it should ignore all other Events and abandon all tasks in the CleanerTasks Queue.
            if (Status != HotelEventType.EVACUATE)
            {
                #region EVACUATE
                //Clears the CurrentTask and the Task Queue (because they need to evacuate)
                //When an EVACUATE event is called the Cleaner calculates the quickest route to the Reception
                if (Event.EventType == HotelEventType.EVACUATE)
                {
                    CurrentTask = null;
                    CleanerTasks.Clear();
                    Status = HotelEventType.EVACUATE;
                    Destination = Hotel.Reception.Node;
                    Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);
                }
                #endregion
                #region CLEANING_EMERGENCY
                //CLEANING_EMERGENCY should be treated as a regular task for the Cleaner
                if (Event.EventType == HotelEventType.CLEANING_EMERGENCY)
                {
                    //In the int[] all the data that is given by the Event will be placed
                    int[] Data = PullIntsFromString(Event.Data.Values.ToList());
                    for (int i = 0; i < GlobalStatistics.Rooms.Count; i++)
                    {
                        //If the given ID matches the ID of a Room, it will be given to the Cleaner as a task
                        if (GlobalStatistics.Rooms[i].ID == Data[0])
                        {
                            //The time that it's take to clean during a CLEANING_EMERGENCY is given withint the Data Dictionairy
                            CleanRoom(new CleanRoom() { RoomToClean = GlobalStatistics.Rooms[i].Node, TimeToClean = Data[1] });
                            break;
                        }
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// Moves the Cleaner to their optimal position inside the Hotel.
        /// </summary>
        private void MoveToOptimalPosition()
        {
            Destination = Graph.SearchNode(Hotel.Floors[Hotel.Floors.Length / GlobalStatistics.Cleaners.Count * CleanerID].Areas[Hotel.Floors[0].Areas.Length / 2]);
            Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Graph.SearchNode(Hotel.Floors[Hotel.Floors.Length / GlobalStatistics.Cleaners.Count * CleanerID].Areas[Hotel.Floors[0].Areas.Length / 2]), true, true);
        }

        /// <summary>
        /// Moves the Cleaner depending on what Task is enqueued and what their Status is.
        /// </summary>
        public void Move()
        {
            //Since the applications is Multi-Threaded (runs on multiple threads due to the HotelEventManager)
            //This can't be added to the HotelEventManager when created through an Event
            //That's why it's called on the Main Application thread and not on the HotelEventManger thread
            if (!IsRegistered)
            {
                HotelEventManager.Register(this);
                IsRegistered = true;
            }

            #region Task Assignment
            //Gives the Cleaner a task if they don't have one and if there's a Queue of CleanerTasks
            if (CurrentTask == null && CleanerTasks.Count > 0)
            {
                CurrentTask = CleanerTasks.Dequeue();
            }
            //If the Cleaner has nothing to do they need to move to their Optimal Position
            else if (CurrentTask == null && Destination == null && CleanerTasks.Count == 0)
            {
                MoveToOptimalPosition();
            }
            #endregion

            #region Pathfinding to Cleaning Task
            //If the Cleaner has a Task and the Destination is null (or the current Position of the Cleaner)
            //The Path will be set for the Cleaner
            if (CurrentTask != null && (Destination == null || Destination == Hotel.Floors[PositionY].Areas[PositionX].Node))
            {
                Destination = CurrentTask.RoomToClean;
                Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);
            }
            #endregion

            #region Performing Cleaning Task
            //If the Cleaner is standing on the Room Node that needs to be cleaned
            //Then the Room's IsDirty state will change to true and the Room will be cleaned
            if (CurrentTask != null && Hotel.Floors[PositionY].Areas[PositionX].Node == CurrentTask.RoomToClean)
            {
                //If the Room isn't being cleaned yet, the Room's IsDirty state will change to true and the cleaning time will be equal to the one set in the Task
                if (((Room)CurrentTask.RoomToClean.Area).CleaningTime == 0 && ((Room)CurrentTask.RoomToClean.Area).IsDirty == true)
                {
                    ((Room)CurrentTask.RoomToClean.Area).CleaningTime = CurrentTask.TimeToClean;
                    IsVisible = false;
                }
                else
                {
                    //If the Room is currently being cleaned then the CleaningTime should go down with 1 (since this method is called every 1 HTE)
                    if (((Room)CurrentTask.RoomToClean.Area).CleaningTime > 0)
                    {
                        ((Room)CurrentTask.RoomToClean.Area).CleaningTime--;
                        //If the Room is done being cleaned, then the Cleaner needs to be visible again and the CurrentTask is done
                        //The Room's IsDirty state will change to false and the room is able to be used again
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
            #endregion

            //There's a better explenation about the PathFinding in the References folder (a document called "Project Hotel - Documentatie.docx")
            if (Path != null)
            {
                #region ToElevator
                //If the Cleaner is in front of the Elevator we will check if it's still efficient to use the Elevator or the Stairs
                if (Path.RouteType == ERouteType.ToElevator && Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft)
                {
                    GetRoute();
                }
                //If the Cleaner is not in front of the Elevator yet, they will walk towards the Elevator by Dequeueing Nodes
                else if (Path.RouteType == ERouteType.ToElevator && Path.PathToElevator.Count != 0)
                {
                    //The Node contains all the info for the Cleaner to move forward (an X and Y co-ordinate)
                    Node moveNode = Path.PathToElevator.Dequeue();
                    PositionX = moveNode.Area.PositionX;
                    PositionY = moveNode.Area.PositionY;
                }
                #endregion

                #region Elevator
                //If the Cleaner has decided to take the Elevator then we're going to try and enter the Elevator
                if (Path.RouteType == ERouteType.Elevator)
                {
                    //If the Cleaner isn't in the Elevator we're going to try and request it
                    if (!IsInElevator)
                    {
                        //If the Cleaner is in front of the Elevator they will enter the Elevator and request the floor (int) that they need to go too
                        if (Hotel.Elevator.GetElevatorInfo().Item2 == PositionY && Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft && !IsInElevator)
                        {
                            //Cleaner Requests the Elevator with their desired Floor
                            Hotel.Elevator.RequestElevator(Destination.Floor);
                            //If the Elevator is on their left side, all they have to do is step to the left (meaning X - 1)
                            PositionX--;

                            IsInElevator = true;
                            //Reset RequestedElevator so that the Cleaner can request the Elevator again
                            RequestedElevator = false;

                            //Add the Cleaner to the Elevator so the Cleaner's position is updated with every HTE with the position of the Elevator
                            Hotel.Elevator.InElevator.Add(this);
                        }
                        //If the Cleaner is in front of the ElevatorShaft they request the Elevator to their current position
                        else if (Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft && !IsInElevator)
                        {
                            if (!RequestedElevator)
                            {
                                Hotel.Elevator.RequestElevator(PositionY);
                                RequestedElevator = true;
                            }
                        }
                    }
                    //If the Cleaner is in the Elevator then we need to check if they need to get out the Elevator or not
                    else
                    {
                        //If the Cleaner is on the Floor (int) that they need to be then she will step out of the Elevator and set their path to FromElevator
                        if (PositionY == Destination.Floor)
                        {
                            Hotel.Elevator.InElevator.Remove(this);
                            Path.RouteType = ERouteType.FromElevator;

                            IsInElevator = false;
                        }
                    }
                }
                #endregion

                #region FromElevator
                //If the Cleaner has stepped out of the Elevator, they need to continue their Path to their Destination
                else if (Path.RouteType == ERouteType.FromElevator && Path.PathFromElevator.Count != 0)
                {
                    //This is done by Dequeueing Node's and setting the Cleaner's current position to that of the Node
                    Node moveNode = Path.PathFromElevator.Dequeue();
                    PositionX = moveNode.Area.PositionX;
                    PositionY = moveNode.Area.PositionY;

                }
                #endregion

                #region Stairs
                //If the Cleaner has decided to take the Stairs instead of the Elevator
                if (Path.RouteType == ERouteType.Stairs)
                {
                    //If the Cleaner needs to wait (due to the StairTime) she will not move
                    if (WaitingTime > 0)
                    {
                        WaitingTime--;
                    }
                    //If the Cleaner doesn't need to wait
                    else
                    {
                        //And the Stair Path is still filled with Node's
                        if (Path.Path.Count != 0)
                        {
                            //By Dequeueing a Node, the Cleaner can move by making their X and Y co-ordinate the same as the Node's
                            Node moveNode = Path.Path.Dequeue();
                            PositionX = moveNode.Area.PositionX;
                            PositionY = moveNode.Area.PositionY;

                            //If the Cleaner moves into a Node, their waiting time should be set to the StairTime (in Settings class)
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

        /// <summary>
        /// Checks how long it will take to use the Elevator and if it's more efficient to take the stairs instead.
        /// </summary>
        private void GetRoute()
        {
            //Has a better explenation in the "Project Hotel - Documentatie.docx" document
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
        
        /// <summary>
        /// Creates a Cleaner and set's their Name to the given string and add's it to the GlobalStatistics
        /// </summary>
        /// <param name="Name">The Name (string) that the Cleaner has.</param>
        /// <returns>IHuman (Cleaner)</returns>
        public IHuman Create(string Name)
        {
            GlobalStatistics.Cleaners.Add(this);
            this.Name = Name;
            return this;
        }

        /// <summary>
        /// Removes all the letters from a given string List and returns all int's in an array (int[])
        /// </summary>
        /// <param name="Data">The string filled List that needs to be converted to an int array (int[])</param>
        /// <returns>An int array (int[]) with all the int's from the given string</returns>
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
