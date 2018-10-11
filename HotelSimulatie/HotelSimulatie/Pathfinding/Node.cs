using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    class Node
    {
        public IArea Area { get; set; }

        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
    }
}
