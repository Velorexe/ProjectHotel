using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    class Reception : HotelEvents.HotelEventListener
    {
        public EAreaType AreaType { get; set; } = EAreaType.Reception;
        public int Capacity { get; set; } = 0;
        public int Height { get; set; } = 1;
        public int Width { get; set; } = 1;
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public void Notify(HotelEvents.HotelEvent hotelEvent)
        {

        }
    }
}
