using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;


public class MapGenerator : MonoBehaviour
{
    public enum MapType
    {
        vacuum = 0,
        wall = 1
    }

    public const int scaleVal = 5;
    public const float offSet = 2.5f; 
    //Given this is using Map itself's local points 
    public event UnityAction mapResponder;
    public SightBox sight;
    public bool[,] map;
    //For debugging purpose 
    public string[,] rawMap; 

    private void Awake()

    {
        GameManager.Map.MapScale = scaleVal;
        GameManager.Map.mapPos = transform; 
        GameManager.Map.SetMap(12, 7);
        GameManager.Map.sight = new SightBox[12, 7];
        CSVParse newFile = new CSVParse("IlDoo/Map/testMap", ',');
        newFile.ParseCSV();
        ConvertRawToGrid(newFile.ParsedData);

        sight = GameManager.Resource.Load<SightBox>("IlDoo/Map/SightBox"); 
    }

    private void Start()
    {
        //ConvertRawToGrid(rawMap);
        PlaceSight(); 
    }

    private void ConvertRawToGrid(string[,] file)
    {
        int row = file.GetLength(0); 
        int col = file.GetLength(1);
        for (int i = 0; i< row; i++)
        {
            for (int j = 0; j< col; j++)
            {
                switch (file[i,j])
                {
                    case "wall":
                        GameManager.Map.Map[i, j] = false; 
                        break;
                    default:
                        GameManager.Map.Map[i, j] = true;
                        break;
                }
            }
        }
    }
    

    private void PlaceSight()
    {
        int row = GameManager.Map.Map.GetLength(0);
        int col = GameManager.Map.Map.GetLength(1);
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                switch (GameManager.Map.Map[i, j])
                {
                    case false:
                        SightBox newSight = GameManager.Resource.Instantiate<SightBox>("IlDoo/Map/SightBox", ConvertGridToMap(j, i), transform.rotation, transform);
                        GameManager.Map.sight[i,j] = newSight;
                        newSight.name = $"x:{i} y:{j}";
                        newSight.SetCoordinate(i, j);
                        newSight.SetGizmo(1); 
                        break;
                    default:
                        newSight = GameManager.Resource.Instantiate<SightBox>("IlDoo/Map/SightBox", ConvertGridToMap(j, i), transform.rotation, transform);
                        GameManager.Map.sight[i, j] = newSight;
                        newSight.name = $"x:{i} y:{j}";
                        newSight.SetCoordinate(i, j);
                        newSight.SetGizmo(0);
                        break;
                }
            }
        }
    }
    private void Attack() 
    {
        mapResponder?.Invoke(); 
        
    }

    private Vector3 ConvertGridToMap(int x, int y)
    {
        Vector3 result = new Vector3(x * scaleVal + offSet, -2.5f, -y * scaleVal + offSet);
        result = transform.localPosition + result;
        return result;
    }
}

public class CSVParse
{
    public string csvFileName;
    public char fieldDelimiter = ',';

    private string[,] parsedData;

    public string[,] ParsedData
    {
        get { return parsedData; }
    }

    public CSVParse(string name, char delimiter)
    {
        this.csvFileName = name;
        this.fieldDelimiter = delimiter;
    }
    public void ParseCSV()
    {
        string filePath = $"{Application.dataPath}/Resources/IlDoo/Map/testMap.csv"; // Assuming the CSV file is in the "Assets" folder

        if (!File.Exists(filePath))
        {
            Debug.LogError("CSV file not found: " + filePath);
            return;
        }

        string[] fileData = File.ReadAllLines(filePath);
        int rows = fileData.Length ; // x
        int columns = fileData[0].Split(fieldDelimiter).Length; // y

        parsedData = new string[columns, rows]; // x, y

        for (int i = 0; i < rows; i++)
        {
            string[] fields = fileData[i].Split(fieldDelimiter);

            for (int j = 0; j < columns; j++)
            {
                parsedData[j, i] = fields[j];
            }
        }
    }
}


