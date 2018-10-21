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
        //All Humans are given a random generated name 
        string Name { get; set; }
        //A boolean to see if the Human must be shown of screen or not
        bool IsVisible { get; set; }

        //PositionX is a point in the grid of the simulation (together with the PositionY it makes a position for the Human)
        int PositionX { get; set; }
        //PositionX is a point in the grid of the simulation (together with the PositionY it makes a position for the Human)
        int PositionY { get; set; }

        //The Route that is given to a Human based on the quickest path to the destination
        Route Path { get; set; }
        //A sprite is given to the Human based on the HumanType
        Bitmap Sprite { get; set; }

        //Because of different threads we need to check if the Human is registered of not.
        //If IsRegistered is true the Human will be added to HotelEventManager
        //If IsRegistered is false nothing will be done to the Human
        bool IsRegistered { get; set; }

        /// <summary>
        /// Creates a instance of IHuman
        /// </summary>
        /// <param name="Name">Name of the IHuman</param>
        /// <returns>IHuman</returns>
        IHuman Create(string Name);
    }
}
