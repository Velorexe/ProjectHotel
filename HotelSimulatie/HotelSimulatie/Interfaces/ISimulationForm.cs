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
        /// 
        /// </summary>
        /// <param name="settings"></param>
        void ApplySettings(Settings settings);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsClosing"></param>
        void PauseSimulation(bool IsClosing);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Areas"></param>
        void HighlightFacility(IArea[] Areas);
    }
}
