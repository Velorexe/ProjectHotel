using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

namespace HotelSimulatie
{
    class ImportLayout
    {
        /// <summary>
        /// Returns a filled Hotel class using a .layout file path.
        /// </summary>
        /// <param name="filePath">The file path (string) of the .layout file.</param>
        /// <returns>Returns a Hotel class</returns>
        public void LayoutImport(string filePath)
        {
            string layout = File.ReadAllText(filePath);

            List<TempLayout> rooms = new List<TempLayout>();
            List<IArea> hotelRooms = new List<IArea>();

            rooms = JsonConvert.DeserializeObject<List<TempLayout>>(layout);

            foreach (TempLayout tempRoom in rooms)
            {
                hotelRooms.Add(RoomFactory.Create
                (
                    tempRoom.ID,
                    tempRoom.AreaType, tempRoom.Capacity,
                    PullIntsFromString(tempRoom.Classification)[0],
                    PullIntsFromString(tempRoom.Position)[0],
                    PullIntsFromString(tempRoom.Position)[1],
                    PullIntsFromString(tempRoom.Dimension)[0],
                    PullIntsFromString(tempRoom.Dimension)[1])
                );
            }

            int maxHeight = 0;
            int maxWidth = 0;
            foreach (IArea area in hotelRooms)
            {
                if (area.PositionX + area.Width > maxWidth)
                    maxWidth = area.PositionX + area.Width;
                if (area.PositionY + area.Height > maxHeight)
                    maxHeight = area.PositionY + area.Height;
            }

            Hotel.Floors = new Floor[maxHeight];
            for (int i = 0; i < Hotel.Floors.Length; i++)
            {
                Hotel.Floors[i] = new Floor(i, maxWidth + 1);
            }
            foreach(IArea area in hotelRooms)
            {
                Hotel.Floors[area.PositionY].Areas[area.PositionX] = area;
            }
            for (int i = 0; i < Hotel.Floors.Length; i++)
            {
                Hotel.Floors[i].Areas[0] = RoomFactory.Create(0, "ElevatorShaft", 0, 0, 0, i, 1, 1);
                Hotel.Floors[i].Areas[Hotel.Floors[i].Areas.Length - 1] = RoomFactory.Create(0, "Staircase", 0, 0, Hotel.Floors[i].Areas.Length - 1, i, 1, 1);
                if (i == 0)
                    Hotel.Floors[i].Areas[1] = RoomFactory.Create(0, "Reception", 0, 0, 1, 0, 1, 1);
                for (int j = 0; j < Hotel.Floors[i].Areas.Length; j++)
                {
                    if (Hotel.Floors[i].Areas[j] is null && i == 0)
                    {
                        Hotel.Floors[i].Areas[j] = RoomFactory.Create(0, "Hallway", 0, 0, j, i, 1, 1);
                        Hotel.Floors[i].Areas[j].Sprite = Sprites.Reception;
                    }
                    else if(Hotel.Floors[i].Areas[j] is null)
                        Hotel.Floors[i].Areas[j] = RoomFactory.Create(0, "Hallway", 0, 0, j, i, 1, 1);
                }
            }
            Hotel.Elevator = new Elevator() { PositionX = 0 , PositionY = 0};
            Hotel.Reception = (Reception)Hotel.Floors[0].Areas[1];
        }

        private int[] PullIntsFromString(string target)
        {
            if (target is null)
            {
                return new int[] { 0, 0 };
            }
            int[] result = new int[2];
            target = target.Replace(" ", "");
            target = Regex.Replace(target, "[A-Za-z ]", "");
            string[] tempArray = target.Split(',');
            for (int i = 0; i < tempArray.Length; i++)
            {
                result[i] = Convert.ToInt32(tempArray[i]);
            }
            return result;
        }

        private class TempLayout
        {
            public int ID { get; set; }
            public string AreaType { get; set; }
            public int Capacity { get; set; }
            public string Position { get; set; }
            public string Dimension { get; set; }
            public string Classification { get; set; }
        }
    }
}
