using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    static class GlobalStatistics
    {
        //The idea behind the GlobalStatistics was to save all the Customers, Cleaners, Rooms and Facilities into one static place
        //So they can be used everywhere in the Application
        //That way we can loop through all the Rooms to assign the right Room to the Customer, etc.
        //And we can assign them as a DataSource for the ReceptionScreen DropDown boxes.

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
