using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    public class Elevator : IMoveAble
    {
        //PositionX is a horizontal point in the grid of the simulation (Together with the PositionY it makes a location for the Elevator)
        public int PositionX { get; set; } = 0;
        //PositionY is a vertical point in the grid of the simulation (Together with the PositionX it makes a location for the Elevator)
        public int PositionY { get; set; } = 0;
        //Sprite for the Elevator
        public Bitmap Sprite { get; set; } = Sprites.Elevator;

        //ELEVATOR FUNCTION
        //'U' for UP, 'D' for DOWN, 'I' for IDLE
        private ElevatorDirection Direction { get; set; } = ElevatorDirection.IDLE;

        //List of the floors the Elevator has to visit when the elevator is goin up
        private List<int> Up = new List<int>();
        //List of the floors the Elevator has to visit when the elevator is goin down
        private List<int> Down = new List<int>();

        //List of Humans inside the Elevator
        public List<IHuman> InElevator = new List<IHuman>();

        //ELEVATOR WORKS LIKE THIS:
        /// <image url="C:\Users\dvddv\Desktop\ProjectHotel\References\Elevator.png" scale="1"/>

        /// <summary>
        /// Get the info from the elevator (Direction and Floor).
        /// </summary>
        /// <returns>The direction (char) that the elevator is going too and the floor (int) that the elevator is currently on.</returns>
        public (ElevatorDirection, int) GetElevatorInfo()
        {
            return (Direction, PositionY);
        }
        
        public void Move()
        {
            if(Direction == ElevatorDirection.IDLE)
            {
                if(Up.Count > Down.Count)
                {
                    Direction = ElevatorDirection.UP;
                }
                else
                {
                    Direction = ElevatorDirection.DOWN;
                }
            }

            if (Up.Count != 0 || Down.Count != 0)
            {
                if (Direction == ElevatorDirection.UP)
                {
                    if (Up.Count == 0 && Down.Count != 0)
                    {
                        this.Direction = ElevatorDirection.DOWN;
                        Move();
                    }
                    else
                    {
                        PositionY++;
                        for (int i = 0; i < Up.Count; i++)
                        {
                            if (Up[i] == PositionY)
                            {
                                Up.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
                else if (Direction == ElevatorDirection.DOWN)
                {
                    if (Up.Count != 0 && Down.Count == 0)
                    {
                        this.Direction = ElevatorDirection.UP;
                        Move();
                    }
                    else
                    {
                        PositionY--;
                        for (int i = 0; i < Down.Count; i++)
                        {
                            if (Down[i] == PositionY)
                            {
                                Down.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
                foreach (IHuman human in InElevator)
                {
                    human.PositionX = PositionX;
                    human.PositionY = PositionY;
                }
            }

            if (Up.Count == 0 && Down.Count == 0)
            {
                Direction = ElevatorDirection.IDLE;
            }
        }

        /// <summary>
        /// Request the elevator to your position
        /// </summary>
        /// <param name="Floor">The Floor that the customer needs to move too</param>
        public void RequestElevator(int RequestFloor)
        {
            //Extra Check
            if (RequestFloor <= Hotel.Floors.Length - 1 && RequestFloor >= 0)
            {
                //Goes UP
                if (RequestFloor > PositionY)
                {
                    Up.Add(RequestFloor);
                }
                //Goes DOWN
                else
                {
                    Down.Add(RequestFloor);
                }
                UpdateList();
            }
        }

        private void UpdateList()
        {
            Up = Up.Distinct().OrderBy(x => x).ToList();
            Down = Down.Distinct().OrderByDescending(x => x).ToList();
        }

    }
        //All possible Elevator Directions
        public enum ElevatorDirection
        {
            UP,
            DOWN,
            IDLE
        }
}
