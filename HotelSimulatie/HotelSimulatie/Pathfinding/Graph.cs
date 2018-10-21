using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    public static class Graph
    {
        //The starting Node of a Graph
        static Node StartNode { get; set; }
        //An array with all the Nodes in the Simulation
        private static Node[,] HotelNodes { get; set; }

        //The documentation for this is given in the Documentation Document (in the References Folder titled "Project Hotel - Documentatie.docx")

        /// <summary>
        /// Creates a Graph with the information from the Hotel
        /// </summary>
        public static void CreateGraph()
        {
            Node[,] hotelNodes = new Node[Hotel.Floors.Length, Hotel.Floors[0].Areas.Length];
            for (int i = 0; i < Hotel.Floors.Length; i++)
            {
                for (int j = 0; j < Hotel.Floors[i].Areas.Length; j++)
                {
                    if (j == 0)
                    {
                        hotelNodes[i, j] = new Node() { Floor = i, NodeType = ENodeType.Elevatorshaft };
                    }
                    else if (j == hotelNodes.GetLength(1) - 1)
                    {
                        hotelNodes[i, j] = new Node() { Floor = i, NodeType = ENodeType.Staircase };
                    }
                    else
                    {
                        hotelNodes[i, j] = new Node() { Floor = i, NodeType = ENodeType.Room };
                    }
                    hotelNodes[i, j].Area = Hotel.Floors[i].Areas[j];
                    Hotel.Floors[i].Areas[j].Node = hotelNodes[i, j];
                }
            }

            for (int i = 0; i < hotelNodes.GetLength(0); i++)
            {
                for (int j = 0; j < hotelNodes.GetLength(1); j++)
                {
                    if (hotelNodes[i, j].Area.AreaType == EAreaType.ElevatorShaft)
                    {
                        if (i == 0)
                        {
                            hotelNodes[i, j] = hotelNodes[i, j].FillBottomNode(Hotel.Floors[i].Areas[j], hotelNodes[i, j + 1], hotelNodes[i + 1, j], false);
                        }
                        else if (i == hotelNodes.GetLength(0) - 1)
                        {
                            hotelNodes[i, j] = hotelNodes[i, j].FillUpperNode(Hotel.Floors[i].Areas[j], hotelNodes[i, j + 1], hotelNodes[i - 1, j], false);
                        }
                        else
                        {
                            hotelNodes[i, j] = hotelNodes[i, j].FillMoveableNode(Hotel.Floors[i].Areas[j], hotelNodes[i, j + 1], hotelNodes[i - 1, j], hotelNodes[i + 1, j], false);
                        }
                        hotelNodes[i, j].Area = Hotel.Floors[i].Areas[j];
                        Hotel.Floors[i].Areas[j].Node = hotelNodes[i, j];
                    }
                    else if (hotelNodes[i, j].Area.AreaType == EAreaType.Staircase)
                    {
                        if (i == 0)
                        {
                            hotelNodes[i, j] = hotelNodes[i, j].FillBottomNode(Hotel.Floors[i].Areas[j], hotelNodes[i, j - 1], hotelNodes[i + 1, j], true);
                        }
                        else if (i == hotelNodes.GetLength(0) - 1)
                        {
                            hotelNodes[i, j] = hotelNodes[i, j].FillUpperNode(Hotel.Floors[i].Areas[j], hotelNodes[i, j - 1], hotelNodes[i - 1, j], true);
                        }
                        else
                        {
                            hotelNodes[i, j] = hotelNodes[i, j].FillMoveableNode(Hotel.Floors[i].Areas[j], hotelNodes[i, j - 1], hotelNodes[i - 1, j], hotelNodes[i + 1, j], true);
                        }
                        hotelNodes[i, j].Area = Hotel.Floors[i].Areas[j];
                        Hotel.Floors[i].Areas[j].Node = hotelNodes[i, j];
                    }
                    else
                    {
                        if (Hotel.Floors[i].Areas[j].AreaType == EAreaType.Reception)
                        {
                            StartNode = hotelNodes[i, j];
                        }
                        hotelNodes[i, j].FillRoomNode(Hotel.Floors[i].Areas[j], hotelNodes[i, j - 1], hotelNodes[i, j + 1]);
                        Hotel.Floors[i].Areas[j].Node = hotelNodes[i, j];
                    }
                }
            }
            HotelNodes = hotelNodes;
            if (StartNode == null)
            {
                StartNode = HotelNodes[0, 0];
            }
        }

        /// <summary>
        /// Searches the Node with the given Area
        /// </summary>
        /// <param name="Area">The Node Area that needs to be found</param>
        /// <returns>The Node with the given Area (Node)</returns>
        public static Node SearchNode(IArea Area)
        {
            for (int i = 0; i < HotelNodes.GetLength(0); i++)
            {
                for (int j = 0; j < HotelNodes.GetLength(1); j++)
                {
                    if (HotelNodes[i, j].Area == Area)
                    {
                        return HotelNodes[i, j];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the quickest route from Node A to Node B
        /// </summary>
        /// <param name="StartingNode">A</param>
        /// <param name="EndNode">B</param>
        /// <param name="ElevatorEnabled">Returns a Route with the Elevator in mind</param>
        /// <param name="StaircaseEnabled">Returns a Route with the Staircase in mind</param>
        /// <returns>The quickest route (Route)</returns>
        public static Route QuickestRoute(Node StartingNode, Node EndNode, bool ElevatorEnabled, bool StaircaseEnabled)
        {
            Route Route = new Route();

            if (EndNode == null)
            {
                System.Diagnostics.Debugger.Break();
            }

            Node CurrentNode = StartingNode;
            if(StartingNode.Floor == EndNode.Floor)
            {
                Route temproute = new Route { RouteType = ERouteType.Stairs };
                if (StartingNode.Area.PositionX < EndNode.Area.PositionX)
                {
                    temproute.Path.Enqueue(CurrentNode);
                    while (CurrentNode != EndNode)
                    {
                        temproute.Path.Enqueue(CurrentNode.RightNode);
                        temproute.PathLength++;
                        CurrentNode = CurrentNode.RightNode;
                    }
                }
                else
                {
                    temproute.Path.Enqueue(CurrentNode);
                    while (CurrentNode != EndNode)
                    {
                        temproute.Path.Enqueue(CurrentNode.LeftNode);
                        temproute.PathLength++;
                        CurrentNode = CurrentNode.LeftNode;
                    }
                }
                return temproute;
            }

            CurrentNode = StartingNode;
            if (ElevatorEnabled)
            {
                Route.PathToElevator.Enqueue(CurrentNode);
                while (CurrentNode != EndNode)
                {
                    while (CurrentNode.NodeType != ENodeType.Elevatorshaft)
                    {
                        Route.PathToElevator.Enqueue(CurrentNode.LeftNode);
                        Route.PathToElevatorLength++;
                        CurrentNode = CurrentNode.LeftNode;
                    }

                    CurrentNode = Hotel.Floors[EndNode.Area.PositionY].Areas[0].Node;
                    while (CurrentNode != EndNode)
                    {
                        Route.PathFromElevator.Enqueue(CurrentNode.RightNode);
                        Route.PathFromElevatorLength++;
                        CurrentNode = CurrentNode.RightNode;
                    }
                }
            }

            CurrentNode = StartingNode;
            if (StaircaseEnabled)
            {
                Route.Path.Enqueue(CurrentNode);
                while (CurrentNode != EndNode)
                {
                    while (CurrentNode.NodeType != ENodeType.Staircase)
                    {
                        Route.Path.Enqueue(CurrentNode.RightNode);
                        Route.PathLength++;
                        CurrentNode = CurrentNode.RightNode;
                        if (EndNode == null)
                        {
                            System.Diagnostics.Debugger.Break();
                        }
                    }


                    while (CurrentNode.Floor != EndNode.Floor)
                    {
                        Route.PathLength += Hotel.Settings.StairCase;
                        if (CurrentNode.Floor < EndNode.Floor)
                        {
                            Route.Path.Enqueue(CurrentNode.UpperNode);
                            CurrentNode = CurrentNode.UpperNode;
                        }
                        else
                        {
                            Route.Path.Enqueue(CurrentNode.LowerNode);
                            CurrentNode = CurrentNode.LowerNode;
                        }
                    }

                    while (CurrentNode != EndNode)
                    {
                        Route.Path.Enqueue(CurrentNode.LeftNode);
                        Route.PathLength++;
                        CurrentNode = CurrentNode.LeftNode;
                    }
                }
            }

            if (ElevatorEnabled && !StaircaseEnabled)
            {
                Route.RouteType = ERouteType.ToElevator;
                return Route;
            }
            else if (StaircaseEnabled && !ElevatorEnabled)
            {
                Route.RouteType = ERouteType.Stairs;
                return Route;
            }
            else if (StaircaseEnabled && ElevatorEnabled)
            {
                if ((Route.PathLength == 0 && Route.PathFromElevatorLength != 0 && Route.PathToElevatorLength != 0) || Route.PathToElevatorLength < Route.PathLength)
                {
                    Route.RouteType = ERouteType.ToElevator;
                    return Route;
                }
                else if (Route.PathLength != 0 && StaircaseEnabled)
                {
                    Route.RouteType = ERouteType.Stairs;
                    return Route;
                }
            }
            return null;
        }

        /// <summary>
        /// Searches the nearest facility with the given AreaType from the given Node
        /// </summary>
        /// <param name="StartingNode">The given Node</param>
        /// <param name="AreaType">The AreaType of the Node</param>
        /// <returns>The nearest facility (Node)</returns>
        public static Node NearestFacility(Node StartingNode, EAreaType AreaType)
        {
            Node CurrentNode = StartingNode;

            HashSet<Node> NearestFacilities = new HashSet<Node>();
            for (int i = 0; i < HotelNodes.GetLength(0); i++)
            {
                for (int j = 0; j < HotelNodes.GetLength(1); j++)
                {
                    if (HotelNodes[i, j].Area.AreaType == AreaType)
                    {
                        NearestFacilities.Add(HotelNodes[i, j]);
                    }
                }
            }

            Node ShortestNode = new Node();
            int ShortestNodeLength = int.MaxValue;

            foreach (Node location in NearestFacilities)
            {
                if (location.Area.PositionX < StartingNode.Area.PositionX && location.Area.PositionY < StartingNode.Area.PositionY)
                {
                    if ((StartingNode.Area.PositionX - location.Area.PositionX) + (StartingNode.Area.PositionY - location.Area.PositionY) < ShortestNodeLength)
                    {
                        ShortestNodeLength = (StartingNode.Area.PositionX - location.Area.PositionX) + (StartingNode.Area.PositionY - location.Area.PositionY);
                        ShortestNode = location;
                    }
                }
                else
                {
                    if ((location.Area.PositionX - StartingNode.Area.PositionX) + (location.Area.PositionY - StartingNode.Area.PositionY) < ShortestNodeLength)
                    {
                        ShortestNodeLength = (StartingNode.Area.PositionX - location.Area.PositionX) + (StartingNode.Area.PositionY - location.Area.PositionY);
                        ShortestNode = location;
                    }
                }
            }

            return ShortestNode;
        }

        /// <summary>
        /// Returns a Route with only the Elevator in mind from Node A to Node B
        /// </summary>
        /// <param name="StartingNode">Node A</param>
        /// <param name="EndNode">Node B</param>
        /// <returns>An Elevator Route (Route)</returns>
        public static Route GetElevatorRoute(Node StartingNode, Node EndNode)
        {
            Route returnPath = new Route();

            Node CurrentNode = StartingNode;
            returnPath.PathToElevator.Enqueue(CurrentNode);
            while (CurrentNode.NodeType != ENodeType.Elevatorshaft)
            {
                returnPath.PathToElevator.Enqueue(CurrentNode.LeftNode);
                returnPath.PathToElevatorLength++;
                CurrentNode = CurrentNode.LeftNode;
            }

            CurrentNode = Hotel.Floors[EndNode.Area.PositionY].Areas[0].Node;
            while (CurrentNode != EndNode)
            {
                returnPath.PathFromElevator.Enqueue(CurrentNode.RightNode);
                returnPath.PathFromElevatorLength++;
                CurrentNode = CurrentNode.RightNode;
            }

            return returnPath;
        }
    }


    class Path
    {
        public Queue<Node> Route = new Queue<Node>();
        public int Length = 0;
    }
}
