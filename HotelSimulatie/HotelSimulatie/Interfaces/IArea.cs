using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    interface IArea
    {
        EAreaType AreaType { get; set; }
        int PositionX { get; set; }
        int PositionY { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        Bitmap Sprite { get; set; }
    }

    public enum EAreaType
    {
        Cinema,
        Restaurant,
        Fitness,
        Reception,
        Room,
        Staircase,
        ElevatorShaft,
        Hallway
    }
}
