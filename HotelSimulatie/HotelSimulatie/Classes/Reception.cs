using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
<<<<<<< HEAD
    class Reception : IArea, HotelEvents.HotelEventListener
=======

    class Reception :  IArea,  HotelEvents.HotelEventListener
>>>>>>> 8d93113528cd78a1edecde47f2fe80ea178b3cd1
    {
        public EAreaType AreaType { get; set; } = EAreaType.Reception;
        public int Height { get; set; } = 1;
        public int Width { get; set; } = 1;
        public int PositionX { get; set; } = 1;
        public int PositionY { get; set; }
<<<<<<< HEAD
=======

>>>>>>> 8d93113528cd78a1edecde47f2fe80ea178b3cd1
        public Bitmap Sprite { get; set; } = Sprites.Reception;

        public void Notify(HotelEvents.HotelEvent hotelEvent)
        {

        }
    }
}
