using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    static class Graph
    {
        static Node StartNode { get; set; }
        private static Node[,] HotelNodes { get; set; }

        public static void CreateGraph()
        {
            Node[,] hotelNodes = new Node[Hotel.Floors.Length, Hotel.Floors[0].Areas.Length];
            for (int i = 0; i < Hotel.Floors.Length; i++)
            {
                for (int j = 0; j < Hotel.Floors[i].Areas.Length; j++)
                {
                    if (j == 0)
                    {
                        hotelNodes[i, j] = new Node() { Floor = i, NodeType = ENodeType.Elevatorshaft};
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
                            hotelNodes[i, j] = hotelNodes[i, j].FillMoveableNode(Hotel.Floors[i].Areas[j], hotelNodes[i, j + 1], hotelNodes[i - 1, j], hotelNodes[i + 1,j], false);
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
                        if(Hotel.Floors[i].Areas[j].AreaType == EAreaType.Reception)
                        {
                            StartNode = hotelNodes[i, j];
                        }
                        hotelNodes[i, j].FillRoomNode(Hotel.Floors[i].Areas[j], hotelNodes[i, j - 1], hotelNodes[i, j + 1]);
                        Hotel.Floors[i].Areas[j].Node = hotelNodes[i, j];
                    }
                }
            }
            HotelNodes = hotelNodes;
            if(StartNode == null)
            {
                StartNode = HotelNodes[0, 0];
            }
        }

        public static Node SearchNode(IArea Area)
        {
            Node currentNode = StartNode;
            for (int i = 0; i < HotelNodes.GetLength(0); i++)
            {
                for (int j = 0; j < HotelNodes.GetLength(1); j++)
                {
                    if(HotelNodes[i,j].Area == Area)
                    {
                        return HotelNodes[i, j];
                    }
                }
            }
            return null;
        }

        public static ElevatorShaft SearchElevatorShaft(int Floor)
        {
            return (ElevatorShaft)HotelNodes[Floor, 0].Area;
        }

        public static Queue<Node> QuickestRoute(Node StartingNode, Node EndNode, bool ElevatorEnabled, bool StaircaseEnabled)
        {
            Path ElevatorRoute = new Path();
            Path StaircaseRoute = new Path();

            Node CurrentNode = StartingNode;
            if (ElevatorEnabled)
            {
                ElevatorRoute.Route.Enqueue(CurrentNode);
                while(CurrentNode != EndNode)
                {
                    while(CurrentNode.NodeType != ENodeType.Elevatorshaft)
                    {
                        ElevatorRoute.Route.Enqueue(CurrentNode.LeftNode);
                        ElevatorRoute.Length++;
                        CurrentNode = CurrentNode.LeftNode;
                    }

                    while(CurrentNode.Floor != EndNode.Floor)
                    {
                        ElevatorRoute.Length = ElevatorRoute.Length + Hotel.Settings.Elevator;
                        if(CurrentNode.Floor < EndNode.Floor)
                        {
                            ElevatorRoute.Route.Enqueue(CurrentNode.UpperNode);
                            CurrentNode = CurrentNode.UpperNode;
                        }
                        else
                        {
                            ElevatorRoute.Route.Enqueue(CurrentNode.LowerNode);
                            CurrentNode = CurrentNode.LowerNode;
                        }
                    }

                    while(CurrentNode != EndNode)
                    {
                        ElevatorRoute.Route.Enqueue(CurrentNode.RightNode);
                        ElevatorRoute.Length++;
                        CurrentNode = CurrentNode.RightNode;
                    }
                }
            }

            CurrentNode = StartingNode;
            if (StaircaseEnabled)
            {
                StaircaseRoute.Route.Enqueue(CurrentNode);
                while (CurrentNode != EndNode)
                {
                    while (CurrentNode.NodeType != ENodeType.Staircase)
                    {
                        StaircaseRoute.Route.Enqueue(CurrentNode.RightNode);
                        StaircaseRoute.Length++;
                        CurrentNode = CurrentNode.RightNode;
                    }

                    while (CurrentNode.Floor != EndNode.Floor)
                    {
                        StaircaseRoute.Length = StaircaseRoute.Length + Hotel.Settings.StairCase;
                        if (CurrentNode.Floor < EndNode.Floor)
                        {
                            StaircaseRoute.Route.Enqueue(CurrentNode.UpperNode);
                            CurrentNode = CurrentNode.UpperNode;
                        }
                        else
                        {
                            StaircaseRoute.Route.Enqueue(CurrentNode.LowerNode);
                            CurrentNode = CurrentNode.LowerNode;
                        }
                    }

                    while (CurrentNode != EndNode)
                    {
                        StaircaseRoute.Route.Enqueue(CurrentNode.LeftNode);
                        StaircaseRoute.Length++;
                        CurrentNode = CurrentNode.LeftNode;
                    }
                }
            }

            if(ElevatorRoute.Length < StaircaseRoute.Length && ElevatorRoute.Length != 0 && ElevatorEnabled)
            {
                return ElevatorRoute.Route;
            }
            else if(StaircaseRoute.Length != 0 && StaircaseEnabled)
            {
                return StaircaseRoute.Route;
            }
            return null;
        }
    }

    class Path
    {
        public Queue<Node> Route = new Queue<Node>();
        public int Length = 0;
    }
}
