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
        /// 
        /// </summary>
        /// <param name="areaType"></param>
        /// <param name="Value"></param>
        /// <param name="IsClosing"></param>
        void ApplyEdits(EAreaType areaType, int Value, bool IsClosing);
    }
}
