using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class MapManager : MonoBehaviour
{
    public event UnityAction React;

    public event UnityAction UponSound;

    public Transform mapPos;

    private int mapScale; 
    public int MapScale
    {
        get { return mapScale; }
        set { mapScale = value; }
    }
    private int fixedHeight; 
    public int FixedHeight
    {
        get { return fixedHeight; }
        set { fixedHeight = value; }
    }

    public SightBox[,] sight; 
    
    private bool[,] map;
    public bool[,] Map
    {
        get { return map; }
        set { map = value; }
    }

    public void SetMap(int x, int y)
    {
        Map = new bool[x, y];
    }
    //Using A* algorithm to compute sound hearer location 

    // TODO: Determine whether listener is actually in listenable distance 
    // CASE 1: Using Coroutine, for every listenable event Invoked.
    // 1. 
    // CASE 2: 
    public List<Sight>? ReturnWayPoint(Sight shotPoint, Sight heardPoint)
    {
        Debug.Log($"ShotPoint {shotPoint.x} {shotPoint.y}");
        Debug.Log($"HeardPoint {heardPoint.x} {heardPoint.y}"); 
        //Convert Sight to the Point 
        Point start = shotPoint.ConvertPoint(); 
        Point end = heardPoint.ConvertPoint();
        List<Point> aStarPath;
        List<Sight> wayPoint = new List<Sight>();
        bool success = Astar.ShortestPath_8(Map, start, end, out aStarPath);
        Debug.Log(success); 
        if (success)
        {
            foreach (Point point in aStarPath)
            {
                Sight sight = new Sight(point);
                wayPoint.Add(sight);
            }
            LightSoundPath(wayPoint);
            return wayPoint;
        }
        return null; 
    }

    public void LightSoundPath(List<Sight> path)
    {
        foreach(Sight obj in path)
        {
            this.sight[obj.x, obj.y].SetGizmo(2);
            Debug.Log($"x{obj.x}y{obj.y}"); 
        }
        React?.Invoke(); 
    }
    //public List<Sight> ReturnWayPoint(Vector3 shotPoint, Vector3 heardPoint)
    //{
    //    List<Point> aStarPath;
    //    Astar.ShortestPath_8(Map, )
    //}
}
