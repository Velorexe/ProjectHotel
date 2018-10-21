using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;
using System.Text.RegularExpressions;

namespace HotelSimulatie
{
    /// <summary>
    /// We can use GlobalEventManager to perform Events that are Global, not for Individuals
    /// </summary>
    public class GlobalEventManager : HotelEventListener
    {
        //A list of the Events that occur
        public List<HotelEvent> EventHistory { get; set; } = new List<HotelEvent>();

        public GlobalEventManager()
        {
            HotelEventManager.Register(this);
        }

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
