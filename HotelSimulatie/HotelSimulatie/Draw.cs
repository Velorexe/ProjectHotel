using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace HotelSimulatie
{
    static class Draw
    {

        public static void DrawTemplate(Bitmap template, int x, int y, PictureBox box)
        {
            using (Graphics e = Graphics.FromImage(template))
            {
                e.DrawImage(template, x, y);
            }
        }
    }
}
