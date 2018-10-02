using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    class ElevatorShaft : IArea
    {
        public EAreaType AreaType { get; set; } = EAreaType.ElevatorShaft;
        public int Height { get; set; } = 1;
        public int Width { get; set; } = 1;
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Capacity { get; set; } = 0;
        public bool ElevatorOnPosition { get; set; } = false;
<<<<<<< HEAD
        public Bitmap Sprite { get; set; } = Sprites.Elevator_Shaft;
=======
        public Bitmap Sprite { get; set; } = HotelSimulatie.Properties.Resources.Elevator_Shaft;
>>>>>>> bdd1359c0e44072f727e5527c6eb9a3248e47f3f
    }
}
