using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    class CleanRoom
    {
        //This is a class that's only used by the Cleaner
        //It contains all the information for the Cleaner to know where to clean and how long it will take

        //Node given to the Area
        public Node RoomToClean { get; set; }
        //How much time it takes for Cleaners to clean the Room
        public int TimeToClean { get; set; }
    }
}
