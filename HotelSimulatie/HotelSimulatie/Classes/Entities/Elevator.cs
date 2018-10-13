using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class Elevator
    {
        public int PositionX { get; set; } = 0;
        public int PositionY { get; set; } 
        public Bitmap Sprite { get; set; } = Sprites.Elevator;

        //ELEVATOR FUNCTION
        //'U' for UP, 'D' for DOWN
        private char Direction { get; set; } = 'U';
        private int Floor { get; set; } = 0;
        
        private List<ElevatorShaft> Up = new List<ElevatorShaft>();
        private List<ElevatorShaft> Down = new List<ElevatorShaft>();

        //ELEVATOR WORKS LIKE THIS:
        /// <image url="C:\Users\dvddv\Desktop\ProjectHotel\References\Elevator.png" scale="1"/>

        /// <summary>
        /// Get the info from the elevator (Direction and Floor).
        /// </summary>
        /// <returns>The direction (char) that the elevator is going too and the floor (int) that the elevator is currently on.</returns>
        public (char, int) GetElevatorInfo()
        {
            return (Direction, Floor);
        }

        /// <summary>
        /// Request the elevator to your position
        /// </summary>
        /// <param name="Floor">The Floor that the customer needs to move too</param>
        public void RequestElevator(int Floor)
        {
            //Goes UP
            if(Floor > this.Floor)
            {
                this.Up.Add(Graph.SearchElevatorShaft(Floor));
                UpdateList('U', this.Floor, Up);
            }
            //Goes DOWN
            else
            {
                this.Down.Add(Graph.SearchElevatorShaft(Floor));
                UpdateList('D', this.Floor, Down);
            }
        }

        private void UpdateList(char Direction, int CurrentFloor, List<ElevatorShaft> Route)
        {
            foreach(ElevatorShaft Shaft in Route)
            {
                if (Direction == 'U')
                {
                    Route.OrderBy(x => x.PositionY);
                }
                else if(Direction == 'D')
                {
                    Route.OrderByDescending(x => x.PositionY);
                }
            }
        }
    }
}
