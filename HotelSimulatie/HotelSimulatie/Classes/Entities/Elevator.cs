using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class Elevator : IMoveAble
    {
        public int PositionX { get; set; } = 0;
        public int PositionY { get; set; } = 0;
        public Bitmap Sprite { get; set; } = Sprites.Elevator;

        //ELEVATOR FUNCTION
        //'U' for UP, 'D' for DOWN
        private char Direction { get; set; } = 'U';
        
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
            return (Direction, PositionY);
        }

        public void Move()
        {
            if(Up.Count != 0 || Down.Count != 0)
            {
                if (Direction == 'U')
                {
                    if (Up.Count == 0 && Down.Count != 0)
                    {
                        this.Direction = 'D';
                        Move();
                    }
                    else
                    {
                        PositionY++;
                        for (int i = 0; i < Up.Count; i++)
                        {
                            if(Up[i].PositionY == PositionY)
                            {
                                Up.RemoveAt(i);
                            }
                        }
                    }
                }
                else if(Direction == 'D')
                {
                    if (Up.Count != 0 && Down.Count == 0)
                    {
                        this.Direction = 'U';
                        Move();
                    }
                    else
                    {
                        PositionY--;
                        for (int i = 0; i < Down.Count; i++)
                        {
                            if (Down[i].PositionY == PositionY)
                            {
                                Down.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Request the elevator to your position
        /// </summary>
        /// <param name="Floor">The Floor that the customer needs to move too</param>
        public void RequestElevator(int CurrentFloor, int TargetFloor)
        {
            //Extra Check
            if ((TargetFloor <= Hotel.Floors.Length - 1 && TargetFloor >= 0) && (CurrentFloor <= Hotel.Floors.Length - 1 && CurrentFloor >= 0))
            {
                //Goes UP
                if (TargetFloor > PositionY)
                {
                    Up.Add(Graph.SearchElevatorShaft(TargetFloor));
                    UpdateList('U', PositionY, Up);
                }
                //Goes DOWN
                else
                {
                    Down.Add(Graph.SearchElevatorShaft(TargetFloor));
                    UpdateList('D', PositionY, Down);
                }

                //Goes UP
                if (CurrentFloor > PositionY)
                {
                    Up.Add(Graph.SearchElevatorShaft(CurrentFloor));
                    UpdateList('U', PositionY, Up);
                }
                //Goes DOWN
                else
                {
                    Down.Add(Graph.SearchElevatorShaft(CurrentFloor));
                    UpdateList('D', PositionY, Down);
                }
            }
        }

        private void UpdateList(char Direction, int CurrentFloor, List<ElevatorShaft> Route)
        {
            foreach(ElevatorShaft Shaft in Route)
            {
                if (Direction == 'U')
                {
                    Up = Route.Distinct().OrderBy(x => x.PositionY).ToList();
                }
                else if(Direction == 'D')
                {
                    Down = Route.Distinct().OrderByDescending(x => x.PositionY).ToList();
                }
            }
        }
    }
}
