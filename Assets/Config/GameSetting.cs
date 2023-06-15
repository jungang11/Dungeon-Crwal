using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        InitManagers(); 
    }

    private static void InitManagers()
    {
        if (GameManager.Instance == null)
        {
            GameObject gameObj = new GameObject() { name = "Game Manager" };
            gameObj.AddComponent<GameManager>();
        }
    }
}
