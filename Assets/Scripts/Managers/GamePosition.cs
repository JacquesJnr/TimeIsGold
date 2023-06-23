using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePosition : MonoBehaviour
{
    [Header("HEIGHT")]
    [SerializeField] private string currentHeight = "0.00";
    
    [Header("LEVEL")]
    [SerializeField] private int currentLevel = 1;
    
    [Header("TRANSFORMS")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform floor;
    
    [Header("EXTENT OFFSET")]
    [SerializeField] float playerExtents;
    
    
    private Vector2 pos;
    public static float CurrentHeight { get; set; }

    private void Update()
    {
        pos = player.position;
        var relativePosition = (pos.y - playerExtents)  - floor.position.y;
        currentHeight = relativePosition.ToString("F2");
        CurrentHeight = relativePosition;
    }
}
