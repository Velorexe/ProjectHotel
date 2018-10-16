using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    static class Hotel
    {
        public static Floor[] Floors { get; set; }
        public static Reception Reception { get; set; }
        public static Settings Settings { get; set; }
        public static Elevator Elevator { get; set; }
        public static GlobalEventManager EventManager { get; set; } = new GlobalEventManager();
    }
}
