using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class RoomFactory
    {
        public IArea Create(string areaType, int capacity,int classification, int positionX, int positionY, int width, int height, Bitmap sprite)
        {
            EAreaType AreaType = StringToAreaType(areaType);
            switch (AreaType)
            {
                case EAreaType.Room:
                    Room tempRoom = new Room();
                    tempRoom.Create(AreaType, capacity, classification, positionX, positionY, width, height, sprite);
                    return tempRoom;
                case EAreaType.Cinema:
                    Cinema tempCinema = new Cinema();
                    tempCinema.Create(AreaType, capacity, classification, positionX, positionY, width, height, sprite);
                    return tempCinema;
                case EAreaType.Elevator:
                    Elevator tempElevator = new Elevator();
                    tempElevator.Create(AreaType, capacity, classification, positionX, positionY, width, height, sprite);
                    return tempElevator;
                case EAreaType.ElevatorShaft:
                    ElevatorShaft tempElevatorShaft = new ElevatorShaft();
                    tempElevatorShaft.Create(AreaType, capacity, classification, positionX, positionY, width, height, sprite);
                    return tempElevatorShaft;
                case EAreaType.Restaurant:
                    Restaurant tempRestaurant = new Restaurant();
                    tempRestaurant.Create(AreaType, capacity, classification, positionX, positionY, width, height, sprite);
                    return tempRestaurant;
                //Reception
                //Staircase
                //Hallway
                //Fitness
            }
            return null;
        }
        private EAreaType StringToAreaType(string parseString)
        {
            EAreaType result = new EAreaType();
            result = (EAreaType)Enum.Parse(typeof(EAreaType), parseString);
            return result;
        }
    }
    
}
