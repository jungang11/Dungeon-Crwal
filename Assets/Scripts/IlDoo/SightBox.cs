using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightBox : MonoBehaviour
{
    //public (int x, int y) ReturnMapCoordinate(Vector3 currentLoc)
    //{

    //}
    public Color color;  
    public Vector3 ReturPosition()
    {
        return transform.position;
    }

    //public Sight ReturnSight(Vector3 currentLoc)
    //{
    //    return null; 
    //}
    private void Awake()
    {
        color = Color.green; 
    }
    public void SetGizmo(int x)
    {
        switch (x)
        {
            case 0:
                color = Color.green; break;
            case 1:
                color = Color.red; break;
            case 2:
                color = Color.blue; break;

        }
    }
    private void Start()
    {
        GameManager.Map.React += Respond;
        GameManager.Map.UponSound += Respond; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color; 
        Gizmos.DrawSphere(transform.position, 1f); 
    }

    public void Respond()
    {
        Gizmos.color = Color.red; 
    }
}

public struct Sight
{
    // Grid coordinates 
    int x;
    int y;
    Vector3 gameLoc; 

    public Sight (int x, int y, Vector3 gameLoc)
    {
        this.x = x; this.y = y; this.gameLoc = gameLoc;
    }

    //private Sight(Vector3 gameLoc)
    //{
        
    //}
}
