using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulatie
{
    public interface IArea
    {
        //Areas are given an ID 
        int ID { get; set; }
        //Areas are given a AreaType based on what is given in the Lay-out file
        EAreaType AreaType { get; set; }
        //PositionX is a horizontal point in the grid of the simulation (Together with the PositionY it makes a location for the Area)
        int PositionX { get; set; }
        //PositionY is a vertical point in the grid of the simulation (Together with the PositionX it makes a location for the Area)
        int PositionY { get; set; }
        //Width of the Area
        int Width { get; set; }
        //Height of the Area
        int Height { get; set; }
        //Areas have different sprites based on the AreaType
        Bitmap Sprite { get; set; }
        //Node given to the Area
        Node Node { get; set; }

        /// <summary>
        /// Creates an instance of IArea with the given Parameters
        /// </summary>
        /// <param name="ID">ID of the Area</param>
        /// <param name="areaType">Type of Area</param>
        /// <param name="capacity">How many Humans can be in the Area at the same time</param>
        /// <param name="classification">The Classification of the Area</param>
        /// <param name="positionX">The horizontal point in the grid</param>
        /// <param name="positionY">The vertical point in the grid</param>
        /// <param name="width">The width of the Area</param>
        /// <param name="height">The height of the Area</param>
        void Create(int ID, EAreaType areaType, int capacity , int classification, int positionX, int positionY, int width, int height);
    }
}
