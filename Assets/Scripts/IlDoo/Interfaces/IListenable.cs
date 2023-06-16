using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListenable
{
    public void Listen(Transform trans);

    public Sight ReturnHeardPosition(Vector3 currentLoc);
    public Sight ReturnHeardPosition();

    public void ReturnHeardPosition(Sight origin); 
}
