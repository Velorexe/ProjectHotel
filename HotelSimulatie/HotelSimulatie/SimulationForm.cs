using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSimulatie
{
    public partial class SimulationForm : Form
    {
        public SimulationForm(string fileLocation)
        {
            InitializeComponent();
            ImportLayout import = new ImportLayout();
            Hotel hotel = import.LayoutImport(fileLocation);
        }
    }
}
