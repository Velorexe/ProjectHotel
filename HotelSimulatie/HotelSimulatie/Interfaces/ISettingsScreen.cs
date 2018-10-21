using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace HotelSimulatie
{
    public interface ISettingsScreen
    {
        /// <summary>
        /// Applies the given edits to the AreaType
        /// </summary>
        /// <param name="areaType">The AreaType that the settings need to apply too</param>
        /// <param name="Value">The new Value for the Parameter</param>
        /// <param name="IsClosing">If the ISettingsScreen is Closing then the eddits shouldn't be applied</param>
        void ApplyEdits(EAreaType areaType, int Value, bool IsClosing);
    }
}
