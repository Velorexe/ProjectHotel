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

        //Elevator Direction
        //ElevatorDirection Enum contains UP, DOWN and IDLE
        //Is set to IDLE on creation
        private ElevatorDirection Direction { get; set; } = ElevatorDirection.IDLE;

        //List of the floors the Elevator has to visit when the elevator is goin up
        private List<int> Up = new List<int>();
        //List of the floors the Elevator has to visit when the elevator is goin down
        private List<int> Down = new List<int>();

        //List of IHumans inside the Elevator
        //Used to update all the IHuman's position everytime the Elevator moves
        public List<IHuman> InElevator = new List<IHuman>();

        //There's an image in the References folder that shows how to Elevator looks to the customer
        //It's called Elevator.PNG

        //For more information about the Elevator, see the References folder and go to the ProjectHotel - Documentatie.docx

        /// <summary>
        /// Gets the info from the elevator (Direction and Floor).
        /// </summary>
        /// <returns>The direction (char) that the elevator is going too and the floor (int) that the elevator is currently on.</returns>
        public (ElevatorDirection, int) GetElevatorInfo()
        {
            return (Direction, PositionY);
        }
        
        /// <summary>
        /// Moves the Elevator based on it's own data.
        /// </summary>
        public void Move()
        {
            #region IDLE
            //If the Direction is IDLE, and there is data inside the UP and DOWN List
            //Then the Elevator needs to change it's Direction depending on what List is bigger (more requests = more important).
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
            #endregion

            if (Up.Count != 0 || Down.Count != 0)
            {
                #region UP
                if (Direction == ElevatorDirection.UP)
                {
                    //Changes Direction if the UP List is empty, but the DOWN List isn't
                    if (Up.Count == 0 && Down.Count != 0)
                    {
                        Direction = ElevatorDirection.DOWN;
                        Move();
                    }
                    else
                    {
                        //Goes one Y position up
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
                #endregion
                #region DOWN
                else if (Direction == ElevatorDirection.DOWN)
                {
                    //Changes Direction if the DOWN List is empty, but the UP List isn't
                    if (Up.Count != 0 && Down.Count == 0)
                    {
                        Direction = ElevatorDirection.UP;
                        Move();
                    }
                    else
                    {
                        //Goes one Y position down
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
                #endregion

                //Moves all the IHuman's that are in the Elevator and updates all their position to the Elevator's
                foreach (IHuman human in InElevator)
                {
                    human.PositionX = PositionX;
                    human.PositionY = PositionY;
                }
            }

            //Sets itself to IDLE if the the UP- and DOWN.Count are 0 (meaning the Elevator has nothing to do)
            if (Up.Count == 0 && Down.Count == 0)
            {
                Direction = ElevatorDirection.IDLE;
            }
        }

        /// <summary>
        /// Request the elevator to the given Floor (int).
        /// </summary>
        /// <param name="Floor">The Floor that the customer needs to move too</param>
        public void RequestElevator(int RequestFloor)
        {
            //Extra Check to see if the Request is within the boundaries of the Hotel
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
                //Updates the Lists
                UpdateList();
            }
        }

        /// <summary>
        /// Updates the UP and DOWN List with LinQ
        /// </summary>
        private void UpdateList()
        {
            Up = Up.Distinct().OrderBy(x => x).ToList();
            Down = Down.Distinct().OrderByDescending(x => x).ToList();
        }
    }
}
