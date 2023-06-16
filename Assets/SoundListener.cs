using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundListener : MonoBehaviour, IListenable
{
    public void Listen(Transform trans)
    {
        throw new System.NotImplementedException();
    }

    public (int x, int y) ReturnMapCoordinate(Vector3 currentLoc)
    {
        throw new System.NotImplementedException();
    }


//    public Sight ReturnSight(Vector3 currentLoc)
//    {
//        currentLoc = transform.localPosition;
//        Sight sight = new Sight(currentLoc); 
//        return Sight(currentLoc); 
//    }
}
