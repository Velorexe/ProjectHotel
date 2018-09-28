using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    public class Hotel
    {
        List<Floor> Floors = new List<Floor>();
    }
    class Floor
    {
        public int FloorHeight { get; set; }
        public List<IArea> Rooms { get; set; }
    }
}
