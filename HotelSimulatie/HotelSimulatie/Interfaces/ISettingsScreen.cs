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
        void ApplyEdits(EAreaType areaType, int Value, bool IsClosing);
    }
}
