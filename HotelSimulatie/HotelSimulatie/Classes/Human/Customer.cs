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
        public Queue<Node> Path { get; set; } = new Queue<Node>();
        public Room AssignedRoom { get; set; } = null;
        public Bitmap Sprite { get; set; } = Sprites.Customer;
        public bool IsInRoom { get; set; } = false;

        private int WaitingTime { get; set; } = 0;

        public void MoveToLocation(IArea CurrentLocation, IArea Destination)
        {
            this.Path = Graph.QuickestRoute(Graph.SearchNode(CurrentLocation), Graph.SearchNode(Destination), false, true);
        }

        public void Move()
        {
            if (WaitingTime > 0)
            {
                WaitingTime--;
            }
            else
            {
                if (Path.Count != 0)
                {
                    Node moveNode = Path.Dequeue();
                    this.PositionX = moveNode.Area.PositionX;
                    this.PositionY = moveNode.Area.PositionY;
                    if(moveNode.Area.AreaType  == EAreaType.Staircase)
                    {
                        WaitingTime = WaitingTime + Hotel.Settings.StairCase - 1;
                    }
                }
                else if (Hotel.Floors[PositionY].Areas[PositionX] == AssignedRoom)
                {
                    this.IsInRoom = true;
                }
                else
                {
                    this.IsInRoom = false;
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
