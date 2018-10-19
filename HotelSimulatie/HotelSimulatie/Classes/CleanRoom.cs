using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    class CleanRoom
    {
        //Node given to the Area
        public Node RoomToClean { get; set; }
        //How much time it takes for Cleaners to clean the Room
        public int TimeToClean { get; set; }
    }
}
