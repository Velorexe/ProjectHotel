using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    static class GlobalStatistics
    {
        public static List<Customer> Customers = new List<Customer>();
        public static List<Cleaner> Cleaners = new List<Cleaner>();

        public static List<Restaurant> Restaurants = new List<Restaurant>();
        public static List<Cinema> Cinemas = new List<Cinema>();
        public static List<Fitness> FitnessCenters = new List<Fitness>();
    }
}
