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


    public Sight ReturnHeardPosition(Vector3 currentLoc)
    {
        currentLoc = Extension.ConvertWorldtoMapPoint(transform); 
        Sight sight = new Sight(currentLoc);
        return sight;
    }

    public void ReturnHeardPosition(Sight origin)
    {

        //Vector3 currentLoc = GameManager.Map.mapPos.InverseTransformPoint(transform.position); 
        Sight sight = new Sight(transform.localPosition.x, transform.localPosition.z);
        GameManager.Map.ReturnWayPoint(origin, sight); 
    }

    public Sight ReturnHeardPosition()
    {
        Sight sight = new Sight(Extension.ConvertWorldtoMapPoint(transform));
        return sight;
    }
}
