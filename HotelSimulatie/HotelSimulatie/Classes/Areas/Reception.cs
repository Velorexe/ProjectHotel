using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace HotelSimulatie
{
    class Reception : IArea, HotelEvents.HotelEventListener
    {
        public EAreaType AreaType { get; set; } = EAreaType.Reception;
        public int Height { get; set; } = 1;
        public int Width { get; set; } = 1;
        public int PositionX { get; set; } = 1;
        public int PositionY { get; set; }
        public Bitmap Sprite { get; set; } = Sprites.ReceptionBar;

        public List<Room> Rooms = new List<Room>();
        public List<IHuman> Customers = new List<IHuman>();
        public List<IHuman> Cleaners = new List<IHuman>();

        public void Create(EAreaType areaType, int capacity, int classification, int positionX, int positionY, int width, int height)
        {
            this.AreaType = areaType;
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Width = width;
            this.Height = height;
            HotelEvents.HotelEventManager.Register(this);
        }

        public void AddAllRooms(Hotel hotel)
        {
            for (int i = 0; i < hotel.Floors.Length; i++)
            {
                for (int j = 0; j < hotel.Floors[i].Areas.Length; j++)
                {
                    if(hotel.Floors[i].Areas[j].AreaType == EAreaType.Room)
                    {
                        this.Rooms.Add((Room)hotel.Floors[i].Areas[j]);
                    }
                }
            }
        }

        public void Notify(HotelEvents.HotelEvent hotelEvent)
        {
            if(hotelEvent.EventType == HotelEvents.HotelEventType.CHECK_IN)
            {
                this.Customers.Add(HumanFactory.CreateHuman(EHumanType.Customer));
            }
        }
    }
}
