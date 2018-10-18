using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    public interface ISimulationForm
    {
        LiveStatistics Statistics { get; set; }

        void ApplySettings(Settings settings);
        void PauseSimulation(bool IsClosing);
        void HighlightFacility(IArea[] Areas);
    }
}
