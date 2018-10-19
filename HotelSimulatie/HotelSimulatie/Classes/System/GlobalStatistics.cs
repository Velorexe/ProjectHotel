using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    static class GlobalStatistics
    {
        //A list of Customers in the Simulation
        public static List<Customer> Customers = new List<Customer>();
        //A list of Cleaners in the Simulation
        public static List<Cleaner> Cleaners = new List<Cleaner>();

        //A list of Restaurants in the Simulation
        public static List<Restaurant> Restaurants = new List<Restaurant>();
        //A list of Cinemas in the Simulation
        public static List<Cinema> Cinemas = new List<Cinema>();
        //A list of Fitness Centers in the Simulation
        public static List<Fitness> FitnessCenters = new List<Fitness>();

        //A list of Rooms in the Simulation
        public static List<Room> Rooms = new List<Room>();
    }
}
