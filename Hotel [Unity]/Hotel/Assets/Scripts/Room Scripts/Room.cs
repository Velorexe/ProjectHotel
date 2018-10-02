using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : IRoom {

	public int Classification { get; set; }
    public string AreaType { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int DimentionX { get; set; }
    public int DimentionY { get; set; }
}
