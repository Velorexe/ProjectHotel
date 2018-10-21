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

        //Nodes are explained in the Documentation Document (In the References Folder titled "Project Hotel - Documentatie.docx")

        public Node FillBottomNode(IArea Area, Node ConnectedNode, Node UpperNode, bool IsStairs)
        {
            this.Area = Area;
            this.UpperNode = UpperNode;

            if (IsStairs)
            {
                LeftNode = ConnectedNode;
                NodeType = ENodeType.Staircase;
            }
            else
            {
                RightNode = ConnectedNode;
                NodeType = ENodeType.Elevatorshaft;
            }
            return this;
        }

        public Node FillUpperNode(IArea Area, Node ConnectedNode, Node LowerNode, bool IsStairs)
        {
            this.Area = Area;
            this.LowerNode = LowerNode;

            if (IsStairs)
            {
                LeftNode = ConnectedNode;
                NodeType = ENodeType.Staircase;
            }
            else
            {
                RightNode = ConnectedNode;
                NodeType = ENodeType.Elevatorshaft;
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
                LeftNode = ConnectedNode;
                NodeType = ENodeType.Staircase;
            }
            else
            {
                RightNode = ConnectedNode;
                NodeType = ENodeType.Elevatorshaft;
            }
            return this;
        }

        public Node FillRoomNode(IArea Area, Node LeftNode, Node RightNode)
        {
            this.Area = Area;
            this.RightNode = RightNode;
            this.LeftNode = LeftNode;
            NodeType = ENodeType.Room;
            return this;
        }
    }
}
