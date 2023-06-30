using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("LEVEL")]
    [SerializeField] private int currentLevel;
    public static int CurrentLevel;
    
    [Header("LEVEL HEIGHTS")]
    [SerializeField] List<float> levelHeights = new List<float>();
    private Dictionary<float, int> levelDictionary = new Dictionary<float, int>();

    public static event Action<int> Change;

    private void Start()
    {
        for (int i = 0; i < levelHeights.Count; i++)
        {
            levelDictionary.Add(levelHeights[i], i+1);
        }

        Change += OnChange;
    }

    private void Update()
    {
        CurrentLevel = UpdateLevel();
        currentLevel = CurrentLevel;
    }

    private int UpdateLevel()
    {
        foreach (var height in levelDictionary.Keys)
        {
            if (GamePosition.CurrentPlayerHeight > height)
            {
                if (HeightToLevel(height) > CurrentLevel)
                {
                    CurrentLevel = HeightToLevel(height);
                    Change!.Invoke(CurrentLevel);
                }
            }
        }
        return CurrentLevel;
    }

    private int HeightToLevel(float height)
    {
        int toLevel = 0;
        levelDictionary.TryGetValue(height, out toLevel);
        return toLevel;
    }

    private void OnChange(int level)
    {
        Debug.Log("Level: " + level);
    }

    private void OnDrawGizmos()
    {
        Vector2 lr = new Vector2(-12, 12);
        
        foreach (var pos in levelHeights)
        {
            Gizmos.DrawLine(new Vector3(lr.x, pos, 0), new Vector3(lr.y,pos,0));
        }
    }
}
