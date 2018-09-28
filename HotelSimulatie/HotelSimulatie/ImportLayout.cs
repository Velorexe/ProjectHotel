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
            rooms = JsonConvert.DeserializeObject<List<TempLayout>>(layout);
            return hotel;
        }

        public int[] PullIntsFromString(string target)
        {
            int[] result = new int[2];
            int index = 0;
            foreach (char letter in target)
            {
                if (Char.IsDigit(letter))
                {
                    result[index] = Convert.ToInt32(letter);
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
