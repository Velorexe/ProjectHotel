using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;
using System.Text.RegularExpressions;
using HotelEvents;

namespace HotelSimulatie
{
    /// <summary>
    /// We can use GlobalEventManager to perform Events that are Global, not for Individuals
    /// </summary>
    public class GlobalEventManager : HotelEventListener
    {
        //The GlobalEventManager was an idea to catch all the HotelEvents and place them in a List
        //Since the Application is Multi Threaded, all the Event's will still play in the background
        //What we wanted to do was catch all those HotelEvents and fire them at all the HotelEventListeners when the correct time is triggered
        //But we didn't have enough time to make this happen, so we put it here to catch the GODZILLA Event
        //Nothing happens in the GODZILLA Event, but it'll still be catched and saved (like all the other Event's)

        //A list of the Events that occur
        public List<HotelEvent> EventHistory { get; set; } = new List<HotelEvent>();

        /// <summary>
        /// Creates a GlobalEventManager and registers it to the HotelEventManager
        /// </summary>
        public GlobalEventManager()
        {
            HotelEventManager.Register(this);
        }

        /// <summary>
        /// An event that's called everytime the HotelEventManager pushes out an HotelEvent. The GlobalEventManager will save all the HotelEvent's
        /// </summary>
        /// <param name="Event">The HotelEvent containing event information.</param>
        public void Notify(HotelEvent Event)
        {
            EventHistory.Add(Event);
            #region GODZILLA
            if (Event.EventType == HotelEventType.GODZILLA)
            {
                //We can perform an action here for the Godzilla event
                //That could the same as an EVACUATE event
                //If it is, we can call this object's own Notify Method with a fake Event
                //this.Notify(new HotelEvent() { EventType = HotelEventType.EVACUATE });
                //All we're missing then is the Time, Message and Data for the Event, but the Evacuation will be performed
            }
            #endregion
        }
    }
}
