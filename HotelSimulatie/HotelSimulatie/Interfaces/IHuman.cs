using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    public interface IHuman
    {
        string Name { get; set; }
        int PositionX { get; set; }
        int PositionY { get; set; }
        Bitmap Sprite { get; set; }

        IHuman Create(string Name);

    }
    public enum EHumanType
    {
        Cleaner,
        Customer
    }
}
