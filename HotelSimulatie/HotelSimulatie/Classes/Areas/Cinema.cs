using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using HotelEvents;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HotelSimulatie
{
    class Cinema : IArea, HotelEventListener
    {
        public int ID { get; set; }
        public EAreaType AreaType { get; set; } = EAreaType.Cinema;

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int MovieTime { get; set; } = 12;
        public bool MovieStarted { get; set; } = false;

        public HashSet<Customer> WaitingLine { get; set; } = new HashSet<Customer>();
        public HashSet<Customer> InCinema { get; set; } = new HashSet<Customer>();

        public Bitmap Sprite { get; set; } = Sprites.Cinema;
        public Node Node { get; set; }

        public void Create(int ID, EAreaType areaType, int capacity,int classification, int positionX, int positionY, int width, int height)
        {
            this.ID = ID;
            this.AreaType = areaType;
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Width = width;
            this.Height = height;
            GlobalStatistics.Cinemas.Add(this);
            HotelEventManager.Register(this);
        }

        public void Notify(HotelEvent Event)
        {
            if(Event.EventType == HotelEventType.START_CINEMA)
            {
                if (Event.Data.Keys.First() == "ID" && PullIntsFromString(Event.Data.Values.ToList())[0] == ID)
                {
                    MovieStarted = true;

                    Sprite = Sprites.Cinema_Start;

                    InCinema = new HashSet<Customer>(WaitingLine);
                    WaitingLine.Clear();

                    foreach (Customer customer in InCinema)
                    {
                        customer.InCinema(MovieTime, this);
                    }
                }
            }
        }

        private int[] PullIntsFromString(List<string> Data)
        {
            int[] result = new int[0];
            for (int j = 0; j < Data.Count; j++)
            {
                string target = Data[j];
                if (target is null)
                {
                    return new int[] { 0, 0 };
                }
                target = target.Replace(" ", "");
                target = Regex.Replace(target, "[A-Za-z ]", "");
                string[] tempArray = target.Split(',');
                result = new int[tempArray.Length];
                for (int i = 0; i < tempArray.Length; i++)
                {
                    result[i] = Convert.ToInt32(tempArray[i]);
                }
            }
            return result;
        }
    }
}
