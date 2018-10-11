using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    class ElevatorShaftNode : Node
    {
        public ElevatorShaft ElevatorShaft { get; set; }

        public new Node RightNode { get; set; }
        public ElevatorShaftNode UpperElevatorShaft { get; set; }
        public ElevatorShaftNode LowerElevatorShaft { get; set; }

        /// <summary>
        /// Fills the top ElevatorShaftNode with the given arguments.
        /// </summary>
        /// <param name="RightNode">The room that the Node is connected too.</param>
        /// <param name="LowerElevatorShaft">The ElevatorShaft that's on top of the Node.</param>
        public ElevatorShaftNode FillUpperElevatorShaftNode(IArea ElevatorShaft, Node RightNode, Node LowerElevatorShaft)
        {
            this.ElevatorShaft = (ElevatorShaft)ElevatorShaft;
            this.RightNode = RightNode;
            this.LowerElevatorShaft = (ElevatorShaftNode)LowerElevatorShaft;
            return this;
        }

        /// <summary>
        /// Fills the bottom ElevatorShaftNode with the given arguments.
        /// </summary>
        /// <param name="RightNode">The room that the Node is connected too.</param>
        /// <param name="UpperElevatorShaft">The ElevatorShaft that's under the Node.</param>
        public ElevatorShaftNode FillLowerElevatorShaftNode(IArea ElevatorShaft, Node RightNode, Node UpperElevatorShaft)
        {
            this.ElevatorShaft = (ElevatorShaft)ElevatorShaft;
            this.RightNode = RightNode;
            this.UpperElevatorShaft = (ElevatorShaftNode)UpperElevatorShaft;
            return this;
        }

        /// <summary>
        /// Fills a ElevatorShaftNode that's inbetween a Bottom- and Top ElevatorShaft with the given arguments.
        /// </summary>
        /// <param name="RightNode">The room that the Node is connected too.</param>
        /// <param name="LowerElevatorShaft">The ElevatorShaft that's under the Node.</param>
        /// <param name="UpperElevatorShaft">The ElevatorShaft that's on top of the Node.</param>
        public ElevatorShaftNode FillElevatorShaftNode(IArea ElevatorShaft, Node RightNode, Node LowerElevatorShaft, Node UpperElevatorShaft)
        {
            this.ElevatorShaft = (ElevatorShaft)ElevatorShaft;
            this.RightNode = RightNode;
            this.LowerElevatorShaft = (ElevatorShaftNode)LowerElevatorShaft;
            this.UpperElevatorShaft = (ElevatorShaftNode)UpperElevatorShaft;
            return this;
        }
    }
}
