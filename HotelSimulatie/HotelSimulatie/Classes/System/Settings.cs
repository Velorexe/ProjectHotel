using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    public class Settings
    {
        public double ZoomLevel { get; set; } = 1;
        public double HTEFactor { get; set; } = 1;
        public int CleaningTime { get; set; } = 2;
        public int StairCase { get; set; } = 2;
        public int Elevator { get; set; } = 1;
        public int EatingTime { get; set; } = 5;
    }
}
