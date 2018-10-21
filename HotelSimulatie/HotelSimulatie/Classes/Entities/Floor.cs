using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    class Floor
    {
        //Int of the Floor level
        public int FloorLevel { get; set; }
        //The amount of Areas a Floor has (how many spaces in X it has)
        public IArea[] Areas { get; set; }

        /// <summary>
        /// When creating the Floor, we can use this to create it with a certain amount of floors.
        /// </summary>
        /// <param name="FloorLevel">The floor level (1st floor, 2nd floor, etc.).</param>
        /// <param name="MaxWidth">The amount of empty IAreas the floor needs to have.</param>
        public Floor(int FloorLevel, int MaxWidth)
        {
            this.FloorLevel = FloorLevel;
            Areas = new IArea[MaxWidth];
        }
    }
}
