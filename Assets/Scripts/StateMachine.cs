using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum GameStates
{
    Alive,
    Dead
}

public enum CameraStates
{
    Waiting,
    Active
}

public class StateMachine : MonoBehaviour
{
    
    [SerializeField] private int currentLevel = 1;
    
    [Header("PLAYER")]
    public static GameStates _gameState;
    [SerializeField] private GameStates gameState;
    public GameObject player;
    
    [Header("CAMERA")]
    public static CameraStates _cameraState;
    [SerializeField] private CameraStates cameraState;

    private void Start()
    {
        _gameState = gameState;
        _cameraState = cameraState;
    }
    
    public static void SetPlayerState(GameStates newState)
    {
        _gameState = newState;
    }

    public static void SetCameraState(CameraStates newState)
    {
        _cameraState = newState;
    }

    private void Update()
    {
        if (_gameState == GameStates.Dead)
        {
            //Time.timeScale = 0;
            SetCameraState(CameraStates.Waiting);
        }
        
    }
}
