using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    public class Settings
    {
        //We wanted to create one general class that can be used to save all the Settings for the Simulation
        //Since the static Hotel Class has a Settings variable, the Settings can be used everywhere in the Application

        //The Zoom-level of the Simulation
        public double ZoomLevel { get; set; } = 1;
        //Speed of the Simulation
        public double HTEFactor { get; set; } = 1;
        //How long it takes Cleaners to clean a room
        public int CleaningTime { get; set; } = 2;
        //How much time it takes Customers to get up 1 set of Stairs
        public int StairCase { get; set; } = 2;
        //How much time the Elevator takes to go up 1 floor
        public int Elevator { get; set; } = 1;
        //How much time the Customers have to wait in line before they die
        public int TimeBeforeDeath { get; set; } = 40;
        //How many Cleaners there are in the Simulation
        public int CleanerAmount { get; set; } = 2;
    }
}
