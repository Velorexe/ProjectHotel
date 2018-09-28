using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    interface IArea
    {
        EAreaType AreaType { get; set; }
        int PositionX { get; set; }
        int PositionY { get; set; }
        int SizeX { get; set; }
        int SizeY { get; set; }
        int Capacity { get; set; }
    }

    public enum EAreaType
    {
        Cinema,
        Restaurant,
        Fitness,
        Reception,
        Room
    }
}
