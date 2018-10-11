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
        public int Floor { get; set; }

        public ENodeType NodeType { get; set; }

        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
        public Node UpperNode { get; set; }
        public Node LowerNode { get; set; }

        public Node FillBottomNode(IArea Area, Node ConnectedNode, Node UpperNode, bool IsStairs)
        {
            this.Area = Area;
            this.UpperNode = UpperNode;

            if (IsStairs)
            {
                this.LeftNode = RightNode;
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
                this.LeftNode = RightNode;
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

    public enum ENodeType
    {
        Staircase,
        Elevatorshaft,
        Room
    }
}
