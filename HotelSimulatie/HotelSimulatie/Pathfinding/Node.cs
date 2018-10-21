using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    public class Node
    {
        //The Area the Node is going to be in
        public IArea Area { get; set; }
        //The Floor the Node is going to be in
        public int Floor { get; set; }

        //The type of Node
        public ENodeType NodeType { get; set; }

        //The Node on the left of a Node that is getting checked
        public Node LeftNode { get; set; }
        //The Node on the right of a Node that is getting checked
        public Node RightNode { get; set; }
        //The Node above a Node that is getting checked
        public Node UpperNode { get; set; }
        //The Node below a Node that is getting checked
        public Node LowerNode { get; set; }

        public Node FillBottomNode(IArea Area, Node ConnectedNode, Node UpperNode, bool IsStairs)
        {
            this.Area = Area;
            this.UpperNode = UpperNode;

            if (IsStairs)
            {
                this.LeftNode = ConnectedNode;
                this.NodeType = ENodeType.Staircase;
            }
            else
            {
                this.RightNode = ConnectedNode;
                this.NodeType = ENodeType.Elevatorshaft;
            }
            return this;
        }

        public Node FillUpperNode(IArea Area, Node ConnectedNode, Node LowerNode, bool IsStairs)
        {
            this.Area = Area;
            this.LowerNode = LowerNode;

            if (IsStairs)
            {
                this.LeftNode = ConnectedNode;
                this.NodeType = ENodeType.Staircase;
            }
            else
            {
                this.RightNode = ConnectedNode;
                this.NodeType = ENodeType.Elevatorshaft;
            }
            return this;
        }

        public Node FillMoveableNode(IArea Area, Node ConnectedNode, Node LowerNode, Node UpperNode, bool IsStairs)
        {
            this.Area = Area;
            this.LowerNode = LowerNode;
            this.UpperNode = UpperNode;

            if (IsStairs)
            {
                this.LeftNode = ConnectedNode;
                this.NodeType = ENodeType.Staircase;
            }
            else
            {
                this.RightNode = ConnectedNode;
                this.NodeType = ENodeType.Elevatorshaft;
            }
            return this;
        }

        public Node FillRoomNode(IArea Area, Node LeftNode, Node RightNode)
        {
            this.Area = Area;
            this.RightNode = RightNode;
            this.LeftNode = LeftNode;
            this.NodeType = ENodeType.Room;
            return this;
        }
    }

    //All possible Node Types
    public enum ENodeType
    {
        Staircase,
        Elevatorshaft,
        Room
    }
}
