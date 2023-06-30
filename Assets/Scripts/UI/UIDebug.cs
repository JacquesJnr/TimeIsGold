using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDebug : MonoBehaviour
{
    public TMP_Text gameState;
    public TMP_Text camState;
    public TMP_Text UIscore;

    private string GameStateActive = "Game State:<color=#0bf702>" + GameStates.Playing.ToString();
    private string GameStateInactive = "Game State:<color=#f70202>" + GameStates.Menu.ToString();
    
    private string CamStateActive = "Camera State:<color=#ff03d9>" + CameraStates.Active.ToString();
    private string CamStateInactive = "Camera State:<color=#001aff>" + CameraStates.Waiting.ToString();

    private void Update()
    {
        switch (StateMachine._gameState)
        {
            case GameStates.Playing:
                gameState.text = GameStateActive;
                break;
            case GameStates.Menu:
                gameState.text = GameStateInactive;
                break;
        }
        
        switch (StateMachine._cameraState)
        {
            case CameraStates.Waiting:
                camState.text = CamStateInactive;
                break;
            case CameraStates.Active:
                camState.text = CamStateActive;
                break;
        }

        UIscore.text =  "Score:<color=#ffe600> " + GameScore.Score;
    }
}
