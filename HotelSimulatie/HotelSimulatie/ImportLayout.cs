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

            for (int i = 0; i < maxHeight + 1; i++)
            {
                hotel.Floors.Add(new Floor(i, maxWidth));
            }

            foreach(IArea area in hotelRooms)
            {
                hotel.Floors[area.PositionY].Areas[area.PositionX] = area;
            }

            //Fill Hotel
            //Set floors to have a list of IAreas
            //Fill the positions
            return hotel;
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
                        Width = PullIntsFromString(tempRoom.Dimention)[0],
                        Height = PullIntsFromString(tempRoom.Dimention)[1]
                    };
                case "Cinema":
                    return new Cinema
                    {
                        AreaType = EAreaType.Cinema,
                        PositionX = PullIntsFromString(tempRoom.Position)[0],
                        PositionY = PullIntsFromString(tempRoom.Position)[1],
                        Width = PullIntsFromString(tempRoom.Dimention)[0],
                        Height = PullIntsFromString(tempRoom.Dimention)[1]
                    };
                case "Restaurant":
                    return new Restaurant
                    {
                        AreaType = EAreaType.Restaurant,
                        PositionX = PullIntsFromString(tempRoom.Position)[0],
                        PositionY = PullIntsFromString(tempRoom.Position)[1],
                        Width = PullIntsFromString(tempRoom.Dimention)[0],
                        Height = PullIntsFromString(tempRoom.Dimention)[1],
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
