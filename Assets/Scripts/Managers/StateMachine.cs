using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum GameStates
{
    Playing,
    Menu
}

public enum CameraStates
{
    Waiting,
    Active
}

public class StateMachine : MonoBehaviour
{
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
        // Player Death
        if (_gameState == GameStates.Menu)
        {
            //Time.timeScale = 0;
            SetCameraState(CameraStates.Waiting);
        }

        // Manual Override
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetCameraState(CameraStates.Active);
        }
    }
}
