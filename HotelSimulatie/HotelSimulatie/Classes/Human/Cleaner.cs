using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using HotelEvents;

namespace HotelSimulatie
{
    class Cleaner : IHuman, IMoveAble, HotelEventListener
    {
        public string Name { get; set; }

        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public bool IsRegistered { get; set; } = false;

        public Route Path { get; set; }
        public Bitmap Sprite { get; set; } = Sprites.Maid;

        public void Notify(HotelEvent Event)
        {

        }

        public void Move()
        {
            if (!IsRegistered)
            {
                HotelEventManager.Register(this);
            }
        }

        public IHuman Create(string Name)
        {
            GlobalStatistics.Cleaners.Add(this);
            this.Name = Name;
            return this;
        }
    }
}
