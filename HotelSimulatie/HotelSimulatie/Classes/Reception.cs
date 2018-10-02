using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
<<<<<<< HEAD
    class Reception :  IArea,  HotelEvents.HotelEventListener
=======
    class Reception : IArea, HotelEvents.HotelEventListener
>>>>>>> bdd1359c0e44072f727e5527c6eb9a3248e47f3f
    {
        public EAreaType AreaType { get; set; } = EAreaType.Reception;
        public int Capacity { get; set; } = 0;
        public int Height { get; set; } = 1;
        public int Width { get; set; } = 1;
        public int PositionX { get; set; }
        public int PositionY { get; set; }
<<<<<<< HEAD
        public Bitmap Sprite { get; set; } = Sprites.Reception;
=======
        public Bitmap Sprite { get; set; } = HotelSimulatie.Properties.Resources.Reception;
>>>>>>> bdd1359c0e44072f727e5527c6eb9a3248e47f3f

        public void Notify(HotelEvents.HotelEvent hotelEvent)
        {

        }
    }
}
