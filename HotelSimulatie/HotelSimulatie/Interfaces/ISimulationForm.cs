using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    public interface ISimulationForm
    {
        //Statistics of the simulation
        LiveStatistics Statistics { get; set; }

        /// <summary>
        /// Sets the new Settings to the Hotel
        /// </summary>
        /// <param name="settings"></param>
        void ApplySettings(Settings settings);

        /// <summary>
        /// Pauses and Continues the Simulation depending on if the ReceptionScreen is open or closing
        /// </summary>
        /// <param name="IsClosing"></param>
        void PauseSimulation(bool IsClosing);

        /// <summary>
        /// Creates a red rectangle around the facilities that are highlighted in the ReceptionScreen
        /// </summary>
        /// <param name="Areas"></param>
        void HighlightFacility(IArea[] Areas);
    }
}
