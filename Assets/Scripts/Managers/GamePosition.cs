using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePosition : MonoBehaviour
{
    [Header("HEIGHT")]
    [SerializeField] private string currentHeight = "0.00";
    private Vector2 pos;
    public static float CurrentPlayerHeight { get; set; }
    
    [Header("TRANSFORMS")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform floor;
    
    [Header("EXTENT OFFSET")]
    [SerializeField] float playerExtents;
    
    private void Update()
    {
        pos = player.position;
        var relativePosition = (pos.y - playerExtents)  - floor.position.y;
        currentHeight = relativePosition.ToString("F2");
        CurrentPlayerHeight = relativePosition;
    }
    
}
