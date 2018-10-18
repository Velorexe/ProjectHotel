using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    class Route
    {
        //Route Type
        public ERouteType RouteType { get; set; } = ERouteType.Undefined;

        //Elevator Route
        public Queue<Node> PathToElevator { get; set; } = new Queue<Node>();
        public int PathToElevatorLength { get; set; }
        public Queue<Node> PathFromElevator { get; set; } = new Queue<Node>();
        public int PathFromElevatorLength { get; set; }

        //Stair Route
        public Queue<Node> Path { get; set; } = new Queue<Node>();
        public int PathLength { get; set; }
    }

    public enum ERouteType
    {
        Stairs,
        Elevator,
        ToElevator,
        FromElevator,
        Undefined
    }
}
