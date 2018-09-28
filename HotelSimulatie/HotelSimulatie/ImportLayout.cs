using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

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
                hotelRooms.Add(ConvertTempToRoom(tempRoom));
            }
            //Fill Hotel
            //Set floors to have a list of IAreas
            //Fill the positions
            return hotel;
        }

        private IArea ConvertTempToRoom(TempLayout tempRoom)
        {
            switch (tempRoom.AreaType)
            {
                case "Room":
                    return new Room
                    {
                        AreaType = EAreaType.Room,
                        Classification = PullIntsFromString(tempRoom.Classification)[0],
                        PositionX = PullIntsFromString(tempRoom.Position)[0],
                        PositionY = PullIntsFromString(tempRoom.Position)[1],
                        SizeX = PullIntsFromString(tempRoom.Dimention)[0],
                        SizeY = PullIntsFromString(tempRoom.Dimention)[1]
                    };
                case "Cinema":
                    return new Cinema
                    {
                        AreaType = EAreaType.Cinema,
                        PositionX = PullIntsFromString(tempRoom.Position)[0],
                        PositionY = PullIntsFromString(tempRoom.Position)[1],
                        SizeX = PullIntsFromString(tempRoom.Dimention)[0],
                        SizeY = PullIntsFromString(tempRoom.Dimention)[1]
                    };
                case "Restaurant":
                    return new Restaurant
                    {
                        AreaType = EAreaType.Restaurant,
                        PositionX = PullIntsFromString(tempRoom.Position)[0],
                        PositionY = PullIntsFromString(tempRoom.Position)[1],
                        SizeX = PullIntsFromString(tempRoom.Dimention)[0],
                        SizeY = PullIntsFromString(tempRoom.Dimention)[1],
                        Capacity = tempRoom.Capacity
                    };
            }
            return null;
        }

        private int[] PullIntsFromString(string target)
        {
            int[] result = new int[2];
            int index = 0;
            foreach (char letter in target)
            {
                if (Char.IsDigit(letter))
                {
                    result[index] = letter - '0';
                    index++;
                }
            }
            return result;
        }

        private class TempLayout
        {
            public string AreaType { get; set; }
            public int Capacity { get; set; }
            public string Position { get; set; }
            public string Dimention { get; set; }
            public string Classification { get; set; }
        }
    }
}
