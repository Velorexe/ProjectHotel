using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;

namespace HotelSimulatie
{
    class ImportLayout
    {
        /// <summary>
        /// Returns a filled Hotel class using a .layout file path.
        /// </summary>
        /// <param name="filePath">The file path (string) of the .layout file.</param>
        /// <returns>Returns a Hotel class</returns>
        public Hotel LayoutImport(string filePath)
        {
            Hotel hotel = new Hotel();
            string layout = File.ReadAllText(filePath);

            List<TempLayout> rooms = new List<TempLayout>();
            List<IArea> hotelRooms = new List<IArea>();

            rooms = JsonConvert.DeserializeObject<List<TempLayout>>(layout);

            foreach(TempLayout tempRoom in rooms)
            {
                hotelRooms.Add(ConvertTempToArea(tempRoom));
            }

            int maxHeight = 0;
            int maxWidth = 0;
            foreach(IArea area in hotelRooms)
            {
                if(area.PositionY + area.Height > maxHeight)
                {
                    maxHeight = area.PositionY + area.Height;
                }
                if(area.PositionX + area.Width > maxWidth)
                {
                    maxWidth = area.PositionX + area.Width;
                }
            }

            for (int i = 0; i < maxHeight; i++)
            {
                hotel.Floors.Add(new Floor(i, maxWidth + 1));
            }

            foreach(IArea area in hotelRooms)
            {
                hotel.Floors[area.PositionY].Areas[area.PositionX] = area;
            }

            for (int i = 0; i < hotel.Floors.Count; i++)
            {
                hotel.Floors[i].Areas[0] = new ElevatorShaft();
                hotel.Floors[i].Areas[hotel.Floors[i].Areas.Count() - 1] = new Staircase();
            }

            hotel.Floors.Reverse();
            hotel.Floors = MoveItemInList(hotel.Floors, hotel.Floors.Count - 1, 0);

            hotel.Floors[0].Areas[1] = new Reception();
        
            return hotel;
        }

        /// <summary>
        /// Moves an item in a given list from one place to the other.
        /// </summary>
        /// <param name="List">The list where the Items need to be changed.</param>
        /// <param name="oldIndex">The index of the Object that needs to be moved.</param>
        /// <param name="newIndex">The new index where the old Object needs to be moved.</param>
        /// <returns>The given list with the Object moved.</returns>
        private List<Floor> MoveItemInList(List<Floor> List, int oldIndex, int newIndex)
        {
            Floor tempFloor = List[oldIndex];
            List.RemoveAt(oldIndex);
            List.Insert(newIndex, tempFloor);
            return List;
        }

        /// <summary>
        /// Converts a temporary room (TempLayout) to an IArea
        /// </summary>
        /// <param name="tempRoom">The temporary room layout that needs to be converted.</param>
        /// <returns>An IArea that corresponds with the given TempLayout's AreaType</returns>
        private IArea ConvertTempToArea(TempLayout tempRoom)
        {
            switch (tempRoom.AreaType)
            {
                //Make this more simple
                //Size, position and everything can be done in one method
                //No need to use a switch case with such complexity everytime
                case "Room":
                    return new Room
                    {
                        AreaType = EAreaType.Room,
                        Classification = PullIntsFromString(tempRoom.Classification)[0],
                        PositionX = PullIntsFromString(tempRoom.Position)[0],
                        PositionY = PullIntsFromString(tempRoom.Position)[1],
                        Width = PullIntsFromString(tempRoom.Dimension)[0],
                        Height = PullIntsFromString(tempRoom.Dimension)[1]
                    };
                case "Cinema":
                    return new Cinema
                    {
                        AreaType = EAreaType.Cinema,
                        PositionX = PullIntsFromString(tempRoom.Position)[0],
                        PositionY = PullIntsFromString(tempRoom.Position)[1],
                        Width = PullIntsFromString(tempRoom.Dimension)[0],
                        Height = PullIntsFromString(tempRoom.Dimension)[1]
                    };
                case "Restaurant":
                    return new Restaurant
                    {
                        AreaType = EAreaType.Restaurant,
                        PositionX = PullIntsFromString(tempRoom.Position)[0],
                        PositionY = PullIntsFromString(tempRoom.Position)[1],
                        Width = PullIntsFromString(tempRoom.Dimension)[0],
                        Height = PullIntsFromString(tempRoom.Dimension)[1],
                        Capacity = tempRoom.Capacity
                    };
                case "Fitness":
                    return new Fitness
                    {
                        AreaType = EAreaType.Fitness,
                        PositionX = PullIntsFromString(tempRoom.Position)[0],
                        PositionY = PullIntsFromString(tempRoom.Position)[1],
                        Width = PullIntsFromString(tempRoom.Dimension)[0],
                        Height = PullIntsFromString(tempRoom.Dimension)[1],
                        Capacity = tempRoom.Capacity
                    };
            }
            return null;
        }

        private int[] PullIntsFromString(string target)
        {
            int[] result = new int[2];
            target = target.Replace(" ", "");
            string[] tempArray = target.Split(',');
            for (int i = 0; i < tempArray.Length; i++)
            {
                result[i] = Convert.ToInt32(tempArray[i]);
            }
            return result;
        }

        private class TempLayout
        {
            public string AreaType { get; set; }
            public int Capacity { get; set; }
            public string Position { get; set; }
            public string Dimension { get; set; }
            public string Classification { get; set; }
        }
    }
}
