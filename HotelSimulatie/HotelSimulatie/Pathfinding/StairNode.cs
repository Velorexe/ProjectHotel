using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    class StairNode : Node
    {
        public Staircase StairCase { get; set; }
        public new int Floor { get; set; }

        public new Node LeftNode { get; set; }
        public StairNode UpperConnectedStair { get; set; }
        public StairNode LowerConnectedStair { get; set; }

        /// <summary>
        /// Fills the bottom StairNode with the given arguments.
        /// </summary>
        /// <param name="StairCase">The IArea that's connected to this Node.</param>
        /// <param name="RightNode">The room that the Node is connected too.</param>
        /// <param name="UpperConnectedStair">The StairNode that's on top of the Node.</param>
        public StairNode FillLowerStairNode(IArea StairCase, Node RightNode, Node UpperConnectedStair)
        {
            this.StairCase = (Staircase)StairCase;
            this.RightNode = RightNode;
            this.UpperConnectedStair = (StairNode)UpperConnectedStair;
            return this;
        }

        /// <summary>
        /// Fills the top ElevatorShaftNode with the given arguments.
        /// </summary>
        /// <param name="StairCase">The IArea that's connected to this Node.</param>
        /// <param name="RightNode">The room that the Node is connected too.</param>
        /// <param name="LowerConnectedStair">The StairNode that's under the Node.</param>
        public StairNode FillUpperStairNode(IArea StairCase, Node RightNode, Node LowerConnectedStair)
        {
            this.StairCase = (Staircase)StairCase;
            this.RightNode = RightNode;
            this.LowerConnectedStair = (StairNode)LowerConnectedStair;
            return this;
        }

        /// <summary>
        /// Fills a StairNode that's inbetween a Bottom- and Top StairNode with the given arguments.
        /// </summary>
        /// <param name="StairCase">The IArea that's connected to this Node.</param>
        /// <param name="RightNode">The room that the Node is connected too.</param>
        /// <param name="LowerConnectedStair">The StairNode that's under the Node.</param>
        /// <param name="UpperConnectedStair">The StairNode that's on top of the Node.</param>
        public StairNode FillStairNode(IArea StairCase, Node RightNode, Node LowerConnectedStair, Node UpperConnectedStair)
        {
            this.StairCase = (Staircase)StairCase;
            this.RightNode = RightNode;
            this.LowerConnectedStair = (StairNode)LowerConnectedStair;
            this.UpperConnectedStair = (StairNode)UpperConnectedStair;
            return this;
        }
    }
}
