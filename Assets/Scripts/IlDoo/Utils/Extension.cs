using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extension
{
    public static Vector3 ConvertWorldtoMapPoint(Transform worldPoint)
    {
        Vector3 rel = worldPoint.position - GameManager.Map.mapPos.position;
        rel.y = GameManager.Map.mapPos.position.y;
        float x = Vector3.Dot(rel, GameManager.Map.mapPos.right);
        float z = Vector3.Dot(rel, GameManager.Map.mapPos.forward);
        return new Vector3(x, rel.y, z);
    }

    public static Vector3 ConvertWorldToLocal(Transform worldPoint)
    {
        Vector3 position = GameManager.Map.mapPos.position;
        position += worldPoint.position.x * GameManager.Map.mapPos.right;
        position += worldPoint.position.z * GameManager.Map.mapPos.forward; 
        return position;
    }

}
