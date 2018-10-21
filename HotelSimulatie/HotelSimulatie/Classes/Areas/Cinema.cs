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
        //Areas are given an ID through the .layout file
        public int ID { get; set; }
        //Areas are given a AreaType based on what is given in the Lay-out file
        public EAreaType AreaType { get; set; } = EAreaType.Cinema;

        //PositionX is a horizontal point in the grid of the simulation (Together with the PositionY it makes a location for the Area)
        public int PositionX { get; set; }
        //PositionY is a vertical point in the grid of the simulation (Together with the PositionX it makes a location for the Area)
        public int PositionY { get; set; }
        //Width of the Area
        public int Width { get; set; }
        //Height of the Area
        public int Height { get; set; }

        //Time how long a movie lasts in the Cinema
        public int MovieTime { get; set; } = 12;
        public bool MovieStarted { get; set; } = false;
        private int MovieProgress { get; set; }

        //Waitingline of Customers
        public HashSet<Customer> WaitingLine { get; set; } = new HashSet<Customer>();
        //List of Customers in the Cinema
        public HashSet<Customer> InCinema { get; set; } = new HashSet<Customer>();

        //Areas have different sprites based on the AreaType
        public Bitmap Sprite { get; set; } = Sprites.Cinema;
        //Node given to the Area
        public Node Node { get; set; }

        /// <summary>
        /// Creation of an Area
        /// </summary>
        /// <param name="ID">ID of the Area</param>
        /// <param name="areaType">Type of Area</param>
        /// <param name="capacity">How many Humans can be in the Area at the same time</param>
        /// <param name="classification">The Classification of the Area</param>
        /// <param name="positionX">The horizontal point in the grid</param>
        /// <param name="positionY">The vertical point in the grid</param>
        /// <param name="width">The width of the Area</param>
        /// <param name="height">The height of the Area</param>
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

        public void Update()
        {
            if (MovieStarted && MovieProgress > 0)
            {
                MovieProgress--;
            }
            if(MovieStarted && MovieProgress <= 0)
            {
                MovieStarted = false;
            }
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
