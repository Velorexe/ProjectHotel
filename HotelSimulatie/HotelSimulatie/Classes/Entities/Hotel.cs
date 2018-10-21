using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    static class Hotel
    {
        //The amount of Floors in the Hotel
        public static Floor[] Floors { get; set; }
        //The Reception of the Hotel
        public static Reception Reception { get; set; }

        //The Settings of the Hotel
        public static Settings Settings { get; set; }
        //The Elevator of the Hotel
        public static Elevator Elevator { get; set; }

        //The Event Manager of the Hotel
        public static GlobalEventManager EventManager { get; set; } = new GlobalEventManager();
    }
}
