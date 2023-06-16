using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SightBox : MonoBehaviour
{
    //public (int x, int y) ReturnMapCoordinate(Vector3 currentLoc)
    //{

    //}
    public int x;
    public int y;
    public Sight grid; 
    public Vector3 thisloc; 
    public Color color;  
    public Vector3 ReturnPosition()
    {
        return transform.position;
    }

    public Vector3 ReturnLocalPosition()
    {
        return transform.localPosition;
    }

    //public Sight ReturnSight(Vector3 currentLoc)
    //{
    //    return null; 
    //}
    private void Awake()
    {
        //GameManager.Map.React += OnDrawGizmos; 
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

    public void SetCoordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    private void Start()
    {
        thisloc = transform.localPosition; 
        //GameManager.Map.React += OnDrawGizmos;
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
    public int x;
    public int y;
    Vector3 gameLoc; 

    public Sight (int x, int y, Vector3 gameLoc)
    {
        this.x = x; this.y = y; this.gameLoc = gameLoc;
    }
    public Sight (float x, float y)
    {
        this.y = Mathf.Abs(Mathf.RoundToInt(x / 5));
        this.x = Mathf.Abs(Mathf.RoundToInt(y / 5));
        this.gameLoc = new Vector3(x, -2.5f, y); 
    }
    public Sight(Vector3 loc, bool mapPoint = true)
    {
        //if (!mapPoint)
        //{
        //    int fixedHeight = GameManager.Map.FixedHeight;
        //    Vector3 result = GameManager.Map.mapPos.InverseTransformPoint(loc);
        //    result.y = fixedHeight;
        //    result = GameManager.Map.mapPos.localPosition + result;
        //    loc = result; 
        //}
        this.x = Mathf.Abs(Mathf.RoundToInt(loc.x / 5));
        this.y = Mathf.Abs(Mathf.RoundToInt(loc.z / 5));
        this.gameLoc = loc;
    }
    public Sight(Point point)
    {
        this.x = point.x; 
        this.y = point.y;
        this.gameLoc = new Vector3(x*5, GameManager.Map.FixedHeight, y*5);
    }

    //Sight => Point 
    public Point ConvertPoint()
    {
        Point point = new Point(this.x, this.y);
        return point; 
    }
}