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
        public static IArea Create(int ID, string areaType, int capacity,int classification, int positionX, int positionY, int width, int height)
        {
            EAreaType AreaType = StringToAreaType(areaType);
            switch (AreaType)
            {
                case EAreaType.Room:
                    Room tempRoom = new Room();
                    tempRoom.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempRoom;
                case EAreaType.Cinema:
                    Cinema tempCinema = new Cinema();
                    tempCinema.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempCinema;
                case EAreaType.ElevatorShaft:
                    ElevatorShaft tempElevatorShaft = new ElevatorShaft();
                    tempElevatorShaft.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempElevatorShaft;
                case EAreaType.Restaurant:
                    Restaurant tempRestaurant = new Restaurant();
                    tempRestaurant.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempRestaurant;
                case EAreaType.Fitness:
                    Fitness tempFitness = new Fitness();
                    tempFitness.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempFitness;
                case EAreaType.Reception:
                    Reception tempReception = new Reception();
                    tempReception.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempReception;
                case EAreaType.Hallway:
                    Hallway tempHallway = new Hallway();
                    tempHallway.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempHallway;
                case EAreaType.Staircase:
                    Staircase tempStaircase = new Staircase();
                    tempStaircase.Create(ID, AreaType, capacity, classification, positionX, positionY, width, height);
                    return tempStaircase;
            }
            return null;
        }
        private static EAreaType StringToAreaType(string parseString)
        {
            EAreaType result = new EAreaType();
            result = (EAreaType)Enum.Parse(typeof(EAreaType), parseString);
            return result;
        }
    }
    
}
