using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar
{
    const int vert_horizontal = 10;
    const int diagonal = 14;
    static Point[] Direction =
    {
            new Point( 0, 1 ),       // Top
            new Point( 0, -1 ),      // Bottom
            new Point( -1, 0 ),      // Left
            new Point( 1, 0 ),       // Right 
            new Point( -1, +1 ),    // Left Top 
			new Point( -1, -1 ),    // Left Bottom 
			new Point( +1, +1 ),    // Right Top 
			new Point( +1, -1 )     // Right bottom  
        };

    // where tile = [y, x] flipped x,y for sake of graphic rendering 
    public static bool ShortestPath_8(bool[,] Map, Point start, Point end, out List<Point> shortestpath)
    {
        int sizeY = Map.GetLength(0);
        int sizeX = Map.GetLength(1);
        shortestpath = new List<Point>();
        bool[,] visited = new bool[sizeY, sizeX];
        PriorityQueue<StarNode, int> contestingNodes = new PriorityQueue<StarNode, int>();
        Stack<StarNode> traceNodes = new Stack<StarNode>();
        // must also have a matrix which saves vertices prior to the 'search'. 
        StarNode[,] nodes = new StarNode[sizeY, sizeX];// �̰� ���߿� route �� �����Ҷ��� �����ϰ� ���ɼ� �ִ�. 

        // Start with the node of the starting position 

        StarNode initial = new StarNode(start, null, 0, Heuristic(start, end));
        contestingNodes.Enqueue(initial, initial.f);

        while (contestingNodes.Count > 0)
        {

            StarNode contestant = contestingNodes.Dequeue();
            //Welcome to the testing ground 
            traceNodes.Push(contestant);
            visited[contestant.point.y, contestant.point.x] = true;
            nodes[contestant.point.y, contestant.point.x] = contestant;
            // if contestant is the final node 
            if (contestant.point.x == end.x && contestant.point.y == end.y)
            {
                // must retrive the route which has been taken to get to the destination 
                Point? toInitial = contestant.point;
                shortestpath = new List<Point>(); 
                // null �� �ƴҶ� ���� path �� �����ؾ߸� �ϴµ�, 
                while (toInitial != null)
                {
                    // 1. path �� �ش� point ���� 
                    // 2. �ش� point �� parent point �� �ݺ����� ���� ���� 
                    Point previous = toInitial.GetValueOrDefault();
                    shortestpath.Add(previous);
                    toInitial = nodes[previous.y, previous.x].parent;
                }
                shortestpath.Reverse();
                return true;
            }

            //���Ŀ��� Ž���ϴ� ������ ��ǥ���� ������ �ֺ����� ���� Ž���� �ʿ��մϴ�. 

            for (int i = 0; i < Direction.Length; i++)
            {
                int new_x = contestant.point.x + Direction[i].x;
                int new_y = contestant.point.y + Direction[i].y;

                // �ش� �ֺ����� ���¸� Ȯ������, �����ٸ� Ž����⿭�� �߰��մϴ� 
                // 1. �̹�Ž���� ���̸� �ȵǰ� 
                // 2. Ÿ�ϱ��� ���ٺҰ������̿����� �ȵǸ� 
                // 3. �ʹۿ� ������ ���̿��� �ȵ˴ϴ�.
                if (new_x < 0 || new_x >= sizeX || new_y < 0 || new_y >= sizeY)
                    continue;
                else if (Map[new_y, new_x] == false)
                    continue;
                else if (visited[new_y, new_x])
                    continue;
                // �밢�� ���� ���ؼ� �߰������� �� Ȯ���Ѵ� 
                else if (DiagonalAlley(Map, contestant.point, i) == false)
                    continue;


                Point temp_point = new Point(new_x, new_y);
                //int g = contestant.g + 10; // for vert/horizontal only 
                int g = contestant.g + (contestant.point.x == end.x || contestant.point.y == end.y ? vert_horizontal : diagonal);
                int h = Heuristic(temp_point, end);
                //���Ŀ� �߰��ϱ��� ���������� �˻��ؾ��Ұ��� ���� �ش� �������� �̹� �߰��� �� ���̶��,
                //�Ǵ�.f ���� ���� ��ǥġ�� �� �����ϴٸ� ����, �ƴ϶�� �����ؾ��մϴ� 

                StarNode temporary = new StarNode(temp_point, contestant.point, g, h);
                if (nodes[new_y, new_x] == null ||
                    nodes[new_y, new_x].f > temporary.f)
                {
                    nodes[new_y, new_x] = temporary;
                    contestingNodes.Enqueue(temporary, temporary.f);
                    traceNodes.Push(temporary);
                }
            }
        }
        //EXTRA: If no path was found. 
        //// if through the loop, false is not given and loop is broken, there's no valid route to destination in this map. 
        //StarNode lastNode = traceNodes.Pop();
        //Point? tracetoFirst = lastNode.point;
        //// null �� �ƴҶ� ���� path �� �����ؾ߸� �ϴµ�, 
        //while (traceNodes.Count > 0)
        //{
        //    // 1. path �� �ش� point ���� 
        //    // 2. �ش� point �� parent point �� �ݺ����� ���� ���� 
        //    Point previous = tracetoFirst.GetValueOrDefault();
        //    shortestpath.Add(previous);
        //    tracetoFirst = traceNodes.Pop().point;
        //}
        shortestpath = null; 
        return false;
    }
    // ���� �밢�� ���� ���ؼ�, ����: �»��� ���, �� �� �� ��� ���� �ִٸ�, false 
    // �װ� �ƴ϶�� true 
    // �밢������ ���ؼ� ����ϴ°� �ƴ϶�� null�� ��ȯ�մϴ�. 
    private static bool? DiagonalAlley(bool[,] tileMap, Point candidate, int direction)
    {
        int left = candidate.x - 1;
        int right = candidate.x + 1;
        int top = candidate.y + 1;
        int bottom = candidate.y - 1;
        if (direction >= 4)
        {
            switch (direction)
            {
                // leftTop 
                case 4:
                    if (tileMap[candidate.y, left] == false && tileMap[top, candidate.x] == false)
                        return false;
                    else
                        return true;
                // leftBottom 
                case 5:
                    if (tileMap[candidate.y, left] == false && tileMap[bottom, candidate.x] == false)
                        return false;
                    else
                        return true;
                // rightTop 
                case 6:
                    if (tileMap[candidate.y, right] == false && tileMap[top, candidate.x] == false)
                        return false;
                    else
                        return true;
                // rightBottom 
                case 7:
                    if (tileMap[candidate.y, right] == false && tileMap[bottom, candidate.x] == false)
                        return false;
                    else
                        return true;
            }

        }
        return null;
    }

    private static int Heuristic(Point start, Point end)
    {
        // Using Euclidean distance for the Heuristic value, in deciding whether contesting node is close to the final destination 
        int x = Math.Abs(start.x - end.x);
        int y = Math.Abs(start.y - end.y);
        int straightCount = Math.Abs(x - y);
        int diagonalCount = Math.Max(x, y) - straightCount;
        return vert_horizontal * straightCount + diagonal * diagonalCount; 
    }
    public class StarNode
    {
        public Point point;
        public Point? parent;
        public int f; // f = g + h
        public int g; // accumulated value from previous routes 
        public int h; // predicted routes based on certain distance calculation Metric

        public StarNode(Point point, Point? parent, int g, int h)
        {
            this.point = point;
            this.parent = parent;
            this.f = g + h;
            this.g = g;
            this.h = h;
        }
    }

}
public struct Point
{
    public int x;
    public int y;
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}