using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    class Floor
    {
        public int FloorLevel { get; set; }
        public IArea[] Areas { get; set; }

        /// <summary>
        /// When creating the Floor, we can use this to create it with a certain amount of floors.
        /// </summary>
        /// <param name="FloorLevel">The floor level (1st floor, 2nd floor, etc.).</param>
        /// <param name="MaxWidth">The amount of empty IAreas the floor needs to have.</param>
        public Floor(int FloorLevel, int MaxWidth)
        {
            this.FloorLevel = FloorLevel;
            this.Areas = new IArea[MaxWidth];
        }
    }
}
