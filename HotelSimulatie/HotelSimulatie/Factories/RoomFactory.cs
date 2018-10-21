using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    public static class RoomFactory
    {
        /// <summary>
        /// Creates an instance of the given AreaType and returns it.
        /// </summary>
        /// <param name="ID">ID of the Area</param>
        /// <param name="areaType">Type of Area</param>
        /// <param name="capacity">How many Humans can be in the Area at the same time</param>
        /// <param name="classification">The Classification of the Area</param>
        /// <param name="positionX">The horizontal point in the grid</param>
        /// <param name="positionY">The vertical point in the grid</param>
        /// <param name="width">The width of the Area</param>
        /// <param name="height">The height of the Area</param>
        /// <returns>An instance of the given AreaType (IArea)</returns>
        public static IArea Create(int ID, string areaType, int capacity,int classification, int positionX, int positionY, int width, int height)
        {
            EAreaType AreaType = StringToAreaType(areaType);
            switch (AreaType)
            {
                #region Facilities
                //If the AreaType is Cinema
                case EAreaType.Cinema:
                    Cinema tempCinema = new Cinema();
                    tempCinema.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempCinema;

                //If the AreaType is Restaurant
                case EAreaType.Restaurant:
                    Restaurant tempRestaurant = new Restaurant();
                    tempRestaurant.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempRestaurant;

                //If the AreaType is Fitness
                case EAreaType.Fitness:
                    Fitness tempFitness = new Fitness();
                    tempFitness.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempFitness;
                #endregion

                #region MoveAble Areas
                //If the AreaType is ElevatorShaft
                case EAreaType.ElevatorShaft:
                    ElevatorShaft tempElevatorShaft = new ElevatorShaft();
                    tempElevatorShaft.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempElevatorShaft;

                //If the AreaType is Hallway
                case EAreaType.Hallway:
                    Hallway tempHallway = new Hallway();
                    tempHallway.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempHallway;

                //If the AreaType is Staircase
                case EAreaType.Staircase:
                    Staircase tempStaircase = new Staircase();
                    tempStaircase.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempStaircase;
                #endregion

                #region Room
                //If the AreaType is Room
                case EAreaType.Room:
                    Room tempRoom = new Room();
                    tempRoom.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempRoom;
                #endregion

                #region Reception
                //If the AreaType is Reception
                case EAreaType.Reception:
                    Reception tempReception = new Reception();
                    tempReception.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempReception;
                #endregion
            }
            //If the given AreaType can't be found we will return null
            return null;
        }

        /// <summary>
        /// Parses a string to an EAreaType
        /// </summary>
        /// <param name="parseString">The string that needs to be parsed</param>
        /// <returns>The EAreaType with the given string (EAreaType)</returns>
        private static EAreaType StringToAreaType(string parseString)
        {
            EAreaType result = new EAreaType();
            result = (EAreaType)Enum.Parse(typeof(EAreaType), parseString);
            return result;
        }
    }
    
}
