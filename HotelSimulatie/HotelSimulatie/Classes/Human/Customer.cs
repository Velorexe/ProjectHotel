using HotelEvents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace HotelSimulatie
{
    public class Customer : IHuman, IMoveAble, HotelEventListener
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

        /// <summary>
        /// Moves the Customer depending on what their Status is.
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

            //Since the Customer can Die, we check if the Customer is waiting and if their DeathTimer does not exceed the given TimeBeforeDeath
            if (IsWaiting == true && DeathTimer >= Hotel.Settings.TimeBeforeDeath)
            {
                //If the Customer needs to die we can remove all instances of him in the Lists
                //That way the C# Garbage Collector will collect it's poor soul
                GlobalStatistics.Customers.Remove(this);
                HotelEventManager.Deregister(this);
            }

            //If the Customer needs to wait for something (Eating, Fitnessing, Taking the Stairs) they will not move until this task is completed (WaitingTime = 0)
            if (WaitingTime > 0)
            {
                WaitingTime--;
            }
            //If the Customer doesn't need to wait for anything to finish
            else if (WaitingTime == 0)
            {
                if (Path != null)
                {
                    #region ToElevator
                    //If the Customer is in front of the Elevator we will check if it's still efficient to use the Elevator or the Stairs
                    if (Path.RouteType == ERouteType.ToElevator && Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft)
                    {
                        GetRoute();
                    }
                    //If the Customer is not in front of the Elevator yet, they will walk towards the Elevator by Dequeueing Nodes
                    else if (Path.RouteType == ERouteType.ToElevator && Path.PathToElevator.Count != 0)
                    {
                        //The Node contains all the info for the Customer to move forward (an X and Y co-ordinate)
                        Node moveNode = Path.PathToElevator.Dequeue();
                        PositionX = moveNode.Area.PositionX;
                        PositionY = moveNode.Area.PositionY;
                    }
                    #endregion

                    #region Elevator
                    if (Path.RouteType == ERouteType.Elevator)
                    {
                        //If the Customer isn't in the Elevator we're going to try and request it
                        if (!IsInElevator)
                        {
                            //If the Customer is in front of the Elevator they will enter the Elevator and request the floor (int) that they need to go too
                            if (Hotel.Elevator.GetElevatorInfo().Item2 == PositionY && Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft && !IsInElevator)
                            {
                                //Customer Requests the Elevator with their desired Floor
                                Hotel.Elevator.RequestElevator(Destination.Floor);
                                //If the Elevator is on their left side, all they have to do is step to the left (meaning X - 1)
                                PositionX--;

                                IsInElevator = true;
                                //Reset RequestedElevator so that the Customer can request the Elevator again
                                RequestedElevator = false;

                                //Add the Customer to the Elevator so the Customer's position is updated with every HTE with the position of the Elevator
                                Hotel.Elevator.InElevator.Add(this);
                            }
                            //If the Customer is in front of the ElevatorShaft they request the Elevator to their current position
                            else if (Hotel.Floors[PositionY].Areas[PositionX - 1].AreaType == EAreaType.ElevatorShaft && !IsInElevator)
                            {
                                if (!RequestedElevator)
                                {
                                    Hotel.Elevator.RequestElevator(PositionY);
                                    RequestedElevator = true;
                                }
                            }
                        }
                        //If the Customer is in the Elevator then we need to check if they need to get out the Elevator or not
                        else
                        {
                            //If the Customer is on the Floor (int) that they need to be then she will step out of the Elevator and set their path to FromElevator
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
                    //If the Customer has stepped out of the Elevator, they need to continue their Path to their Destination
                    else if (Path.RouteType == ERouteType.FromElevator && Path.PathFromElevator.Count != 0)
                    {
                        //This is done by Dequeueing Node's and setting the Customer's current position to that of the Node
                        Node moveNode = Path.PathFromElevator.Dequeue();
                        PositionX = moveNode.Area.PositionX;
                        PositionY = moveNode.Area.PositionY;
                    }
                    #endregion

                    #region Stairs
                    //If the Customer has decided to take the Stairs instead of the Elevator
                    if (Path.RouteType == ERouteType.Stairs)
                    {
                        //And the Stair Path is still filled with Node's
                        if (Path.Path.Count != 0)
                        {
                            //By Dequeueing a Node, the Customer can move by making their X and Y co-ordinate the same as the Node's
                            Node moveNode = Path.Path.Dequeue();
                            PositionX = moveNode.Area.PositionX;
                            PositionY = moveNode.Area.PositionY;

                            //If the Customer moves into a Node, their waiting time should be set to the StairTime (StairTime can be set in the ReceptionScreen)
                            if (moveNode.Area.AreaType == EAreaType.Staircase)
                            {
                                WaitingTime = WaitingTime + Hotel.Settings.StairCase - 1;
                            }
                        }
                    }
                    #endregion
                }
                //If the Path is null (for some reason) the Customer goes back to their Room
                else
                {
                    Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, AssignedRoom.Node, true, true);
                }

                //If the Customer doesn't have anywhere to go, they will get their QuickestRoute to their Room
                if (Destination == null)
                {
                    Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, AssignedRoom.Node, true, true);
                }
            }

            //If the Customer is not in an IArea then it should be drawn
            if (InArea == null)
            {
                IsVisible = true;
            }
            //If the Customer isn't in an IArea then it shouldn't be drawn
            else
            {
                IsVisible = false;
            }

            //If the Customer has arrived on their Destination
            if (Hotel.Floors[PositionY].Areas[PositionX].Node == Destination)
            {
                //If the Destination is a Restaurant
                if (Destination.Area.AreaType == EAreaType.Restaurant)
                {
                    //They will enter the Area and set their WaitingTime to the Restaurant's EatingTime (EatingTime can be changed for every restaurant)
                    WaitingTime = ((Restaurant)Destination.Area).EatingTime;
                    InArea = Destination.Area;

                    //Their Destination is set to their Room
                    //If the Customer is done Eating they will automatically go back to their Room
                    Destination = AssignedRoom.Node;
                    Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);
                }
                //If the Destination is a Cinema
                else if (Destination.Area.AreaType == EAreaType.Cinema)
                {
                    //The Customer will check if the Movie has started or not
                    if (!((Cinema)Destination.Area).MovieStarted)
                    {
                        //If the Movie hasn't started it will put itself in the WaitingLine of the Cinema
                        ((Cinema)Destination.Area).WaitingLine.Add(this);
                        //The IsWaiting will be set to true (Customers can die if they wait too long)
                        IsWaiting = true;
                    }
                    //If the Movie has already started, poor Customer :( 
                    else
                    {
                        //Their Destination will be set to their Room and they'll travel back to it
                        Destination = AssignedRoom.Node;
                        Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);
                    }
                }
                //If the Destination is a Fitness
                else if(Destination.Area.AreaType == EAreaType.Fitness)
                {
                    //Their WaitingTime will be set to their FitnessTime and they'll enter the Area
                    //FitnessTime is given with the GOTO_FITNESS HotelEvent
                    WaitingTime = FitnessTime;
                    InArea = Destination.Area;

                    //Their Destination is set to their Room
                    //If the Customer is done Fitnessing they will automatically go back to their Room
                    Destination = AssignedRoom.Node;
                    Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);
                }
                //If the Destination is their AssignedRoom
                else if (Hotel.Floors[PositionY].Areas[PositionX] == AssignedRoom)
                {
                    //It will enter their Room
                    InArea = AssignedRoom;
                }
            }
            else if(WaitingTime == 0)
            {
                InArea = null;
            }

            //If the Customer's status is CHECK_OUT (meaning they want to check out) and they're standing on the Reception
            if (Status == HotelEventType.CHECK_OUT && Hotel.Floors[PositionY].Areas[PositionX] == Hotel.Reception)
            {
                //They'll remove themselves from any Lists reffering to them and the Garbage Collector will delete them from existence
                GlobalStatistics.Customers.Remove(this);
                HotelEventManager.Deregister(this);
            }

            #region DeathTimer
            //We check if their current position is the same as their last one
            //If that's true and their not inside an Area
            if (LastLocation == Hotel.Floors[PositionY].Areas[PositionX].Node && InArea == null)
            {
                //The IsWaiting will be set to true and the DeathTimer increases
                IsWaiting = true;
                DeathTimer++;
            }
            //If this is false
            else
            {
                //The IsWaiting will be set to false and their DeathTimer will be reset
                IsWaiting = false;
                DeathTimer = 0;
            }
            //And their LastLocation will be saved
            LastLocation = Hotel.Floors[PositionY].Areas[PositionX].Node;
            #endregion
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

        /// <summary>
        /// Creates a Customer and set's their Name to the given string and add's it to the GlobalStatistics
        /// </summary>
        /// <param name="Name">The Name (string) that the Customer has.</param>
        /// <returns>IHuman (Customer)</returns>
        public IHuman Create(string Name)
        {
            GlobalStatistics.Customers.Add(this);
            this.Name = Name;
            return this;
        }

        /// <summary>
        /// An event that's called everytime the HotelEventManager pushes out an HotelEvent.
        /// </summary>
        /// <param name="Event">The HotelEvent containing event information.</param>
        public void Notify(HotelEvent Event)
        {
            //If the Customer is currently Evacuating they shouldn't listen to any other Events
            if (Status != HotelEventType.EVACUATE)
            {
                //If the Customer needs to Evacuate
                if(Event.EventType == HotelEventType.EVACUATE)
                {
                    //The Customer's WaitingTime is set to 0 (they need to abandon all current tasks)
                    WaitingTime = 0;
                    //The Customer's Status is set to EVACUATE so other Events won't be triggered
                    Status = HotelEventType.EVACUATE;

                    //And they'll EVACUATE to the Reception
                    Destination = Hotel.Reception.Node;
                    Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);
                }
                //If the Customer needs to hit the gym
                else if(Event.EventType == HotelEventType.GOTO_FITNESS)
                {
                    //We will perform an extra check to see if it's aimed at a "Gast" (Customer)
                    if (Event.Data.Keys.First() == "Gast")
                    {
                        //If that's true, we will pull the int's from the HotelEvent Data Dictionairy
                        int[] Data = PullIntsFromString(Event.Data.Values.ToList());
                        //If the given ID is the same as the Customer's ID
                        if (ID == Data[0])
                        {
                            //The Customer's FitnessTime will be set to the one given inside the HotelEvent
                            FitnessTime = Data[1];

                            //The Customer's Destination is set to the nearest Gym
                            Destination = Graph.NearestFacility(Hotel.Floors[PositionY].Areas[PositionX].Node, EAreaType.Fitness);
                            Path = Graph.QuickestRoute(Graph.SearchNode(Hotel.Floors[PositionY].Areas[PositionX]), Destination, true, true);

                            //The Customer's Status is set to GOTO_FITNESS
                            Status = HotelEventType.GOTO_FITNESS;
                        }
                    }
                }
                //If the Customer needs to check out
                else if (Event.EventType == HotelEventType.CHECK_OUT)
                {
                    //We will perform an extra check to see if it's aimed at a "Gast" (Customer)
                    if (Event.Data.Keys.First() == "Gast")
                    {
                        //If that's true, we will pull the int's from the HotelEvent Data Dictionairy
                        int[] Data = PullIntsFromString(Event.Data.Values.ToList());
                        //If the given ID is the same as the Customer's ID
                        if (ID == Data[0])
                        {
                            //If the Customer checks out, their Room will be Dirty and needs to be cleaned
                            AssignedRoom.Dirty();
                            //Their Room will be set to Avaiable (by saying that the Room doesn't have an owner)
                            AssignedRoom.RoomOwner = null;

                            //The Customer will go to the Reception (the Reception won't do anything with the Customer, they'll dissapear if they arrive)
                            Destination = Hotel.Reception.Node;
                            Path = Graph.QuickestRoute(Graph.SearchNode(Hotel.Floors[PositionY].Areas[PositionX]), Destination, true, true);

                            //The Customer's Status is set to CHECK_OUT
                            Status = HotelEventType.CHECK_OUT;
                        }
                    }
                }
                //If the Customer wants to go to a Cinema to catch the new Shrek Movie
                else if (Event.EventType == HotelEventType.GOTO_CINEMA)
                {
                    //We will perform an extra check to see if it's aimed at a "Gast" (Customer)
                    if (Event.Data.Keys.First() == "Gast")
                    {
                        //If that's true, we will pull the int's from the HotelEvent Data Dictionairy
                        int[] Data = PullIntsFromString(Event.Data.Values.ToList());
                        //If the given ID is the same as the Customer's ID
                        if (ID == Data[0])
                        {
                            //The Customer will move to the nearest Cinema (relative to their Position)
                            Destination = Graph.NearestFacility(Hotel.Floors[PositionY].Areas[PositionX].Node, EAreaType.Cinema);
                            Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);

                            //The Customer's Status is set to GOTO_CINEMA
                            Status = HotelEventType.GOTO_CINEMA;
                        }
                    }
                }
                //If the Customer needs food
                else if (Event.EventType == HotelEventType.NEED_FOOD)
                {
                    //We will perform an extra check to see if it's aimed at a "Gast" (Customer)
                    if (Event.Data.Keys.First() == "Gast")
                    {
                        //If that's true, we will pull the int's from the HotelEvent Data Dictionairy
                        int[] Data = PullIntsFromString(Event.Data.Values.ToList());
                        //If the given ID is the same as the Customer's ID
                        if (ID == Data[0])
                        {
                            //The Customer will move to the nearest Cinema (relative to their Position)
                            Destination = Graph.NearestFacility(Hotel.Floors[PositionY].Areas[PositionX].Node, EAreaType.Restaurant);
                            Path = Graph.QuickestRoute(Hotel.Floors[PositionY].Areas[PositionX].Node, Destination, true, true);

                            //The Customer's Status is set to NEED_FOOD
                            Status = HotelEventType.NEED_FOOD;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sets the Customer in the Cinema and set's their WaitingTime to the Cinema's MovieTime
        /// </summary>
        /// <param name="MovieTime">The time that the Movie lasts (int)</param>
        /// <param name="Area">The Cinema's Area (Cinema)</param>
        public void InCinema(int MovieTime, Cinema Area)
        {
            //Customer's WaitingTime is set to the Cinema's MovieTime
            WaitingTime = MovieTime;
            //Their InArea is set to the Cinema (they won't be drawn on screen)
            InArea = Area;
            //Since the Customer shouldn't die while being in the Cinema, they won't be waiting
            IsWaiting = false;
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
