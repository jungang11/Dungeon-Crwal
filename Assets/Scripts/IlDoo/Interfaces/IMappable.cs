using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMappable
{
    public (int x, int y) ReturnMapCoordinate(Vector3 currentLoc); 

    public Sight ReturnSight(Vector3 currentLoc);
}
