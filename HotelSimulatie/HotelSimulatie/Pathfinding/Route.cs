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

        //The path made out of Nodes between the position of the Human and the Elevator
        public Queue<Node> PathToElevator { get; set; } = new Queue<Node>();
        //The length of the path between the position of the Human and the Elevator
        public int PathToElevatorLength { get; set; }
        //The path made out of Nodes between the position of the Elevator and the destination of the Human 
        public Queue<Node> PathFromElevator { get; set; } = new Queue<Node>();
        //The length of the path between the Elevator and the destination of the Human
        public int PathFromElevatorLength { get; set; }

        //The path made out of Nodes between the Human and the destination of the Human if the Stairs are being used
        public Queue<Node> Path { get; set; } = new Queue<Node>();
        //The lenght of the path between the Human and the destination of the Human if the Stairs are being used
        public int PathLength { get; set; }
    }

    //All the possible RouteTypes
    public enum ERouteType
    {
        Stairs,
        Elevator,
        ToElevator,
        FromElevator,
        Undefined
    }
}
