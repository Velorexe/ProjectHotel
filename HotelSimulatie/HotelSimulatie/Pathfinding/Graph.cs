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

        public static void CreateGraph(Hotel hotel)
        {
            Node[,] hotelNodes = new Node[hotel.Floors.Length, hotel.Floors[0].Areas.Length];
            for (int i = 0; i < hotel.Floors.Length; i++)
            {
                for (int j = 0; j < hotel.Floors[i].Areas.Length; j++)
                {
                    if (j == 0)
                    {
                        hotelNodes[i, j] = new ElevatorShaftNode() { Floor = i };
                    }
                    else if (j == hotelNodes.GetLength(1) - 1)
                    {
                        hotelNodes[i, j] = new StairNode() { Floor = i };
                    }
                    else
                    {
                        hotelNodes[i, j] = new Node() { Floor = i };
                    }
                    hotelNodes[i, j].Area = hotel.Floors[i].Areas[j];
                    hotel.Floors[i].Areas[j].Node = hotelNodes[i, j];
                }
            }

            for (int i = 0; i < hotelNodes.GetLength(0); i++)
            {
                for (int j = 0; j < hotelNodes.GetLength(1); j++)
                {
                    if (hotelNodes[i, j].Area.AreaType == EAreaType.ElevatorShaft)
                    {
                        ElevatorShaftNode tempNode = (ElevatorShaftNode)hotelNodes[i, j];
                        if (i == 0)
                        {
                            hotelNodes[i, j] = tempNode.FillLowerElevatorShaftNode(hotel.Floors[i].Areas[j], hotelNodes[i, j + 1], hotelNodes[i + 1, j]);
                        }
                        else if (i == hotelNodes.GetLength(0) - 1)
                        {
                            hotelNodes[i, j] = tempNode.FillUpperElevatorShaftNode(hotel.Floors[i].Areas[j], hotelNodes[i, j + 1], hotelNodes[i - 1, j]);
                        }
                        else
                        {
                            hotelNodes[i, j] = tempNode.FillElevatorShaftNode(hotel.Floors[i].Areas[j], hotelNodes[i, j + 1], hotelNodes[i - 1, j], hotelNodes[i + 1,j]);
                        }
                        hotelNodes[i, j].Area = hotel.Floors[i].Areas[j];
                        hotel.Floors[i].Areas[j].Node = hotelNodes[i, j];
                    }
                    else if (hotelNodes[i, j].Area.AreaType == EAreaType.Staircase)
                    {
                        StairNode tempNode = (StairNode)hotelNodes[i, j];
                        if (i == 0)
                        {
                            hotelNodes[i, j] = tempNode.FillLowerStairNode(hotel.Floors[i].Areas[j], hotelNodes[i, j - 1], hotelNodes[i + 1, j]);
                        }
                        else if (i == hotelNodes.GetLength(0) - 1)
                        {
                            hotelNodes[i, j] = tempNode.FillUpperStairNode(hotel.Floors[i].Areas[j], hotelNodes[i, j - 1], hotelNodes[i - 1, j]);
                        }
                        else
                        {
                            hotelNodes[i, j] = tempNode.FillStairNode(hotel.Floors[i].Areas[j], hotelNodes[i, j - 1], hotelNodes[i - 1, j], hotelNodes[i + 1, j]);
                        }
                        hotelNodes[i, j].Area = hotel.Floors[i].Areas[j];
                        hotel.Floors[i].Areas[j].Node = hotelNodes[i, j];
                    }
                    else
                    {
                        if(hotel.Floors[i].Areas[j].AreaType == EAreaType.Reception)
                        {
                            StartNode = hotelNodes[i, j];
                        }
                        hotelNodes[i, j].LeftNode = hotelNodes[i, j - 1];
                        hotelNodes[i, j].RightNode = hotelNodes[i, j + 1];
                        hotel.Floors[i].Areas[j].Node = hotelNodes[i, j];
                    }
                }
            }
            HotelNodes = hotelNodes;
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

        public static Queue<Node> QuickestRoute(Node StartingNode, Node EndNode, bool ElevatorEnabled, bool StaircaseEnabled)
        {
            Queue<Node> ElevatorRoute = new Queue<Node>();
            Queue<Node> StaircaseRoute = new Queue<Node>();

            Node CurrentNode = StartingNode;
            
            if (ElevatorEnabled)
            {
                while(CurrentNode != EndNode)
                {

                }
            }
            if (StaircaseEnabled)
            {
                while(CurrentNode != EndNode)
                {

                }
            }

            if(ElevatorRoute.Count < StaircaseRoute.Count && ElevatorRoute.Count != 0)
            {
                return ElevatorRoute;
            }
            else if(StaircaseRoute.Count != 0)
            {
                return StaircaseRoute;
            }
            return null;
        }
    }

    class Path
    {
        Queue<Node> Route = new Queue<Node>();
        int Length = 0;
    }
}
