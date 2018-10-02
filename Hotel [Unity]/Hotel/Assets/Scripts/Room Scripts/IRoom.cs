using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoom{
    
    string AreaType { get; set; }
    int PositionX { get; set; }
    int PositionY { get; set; }
    int DimentionX { get; set; }
    int DimentionY { get; set; }
}
