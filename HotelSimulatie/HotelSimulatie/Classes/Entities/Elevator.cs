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
        //'U' for UP, 'D' for DOWN, 'I' for IDLE
        private ElevatorDirection Direction { get; set; } = ElevatorDirection.IDLE;

        private List<int> Up = new List<int>();
        private List<int> Down = new List<int>();

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
        public enum ElevatorDirection
        {
            UP,
            DOWN,
            IDLE
        }
}
