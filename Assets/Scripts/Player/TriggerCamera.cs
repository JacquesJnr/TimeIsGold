using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCamera : MonoBehaviour
{
    [SerializeField] private LayerMask triggerLayer;
    [SerializeField] private bool onTrigger;
    [SerializeField] private Vector2 bounds;
    
    // Update is called once per frame
    void Update()
    {
        // Trigger Camera
        onTrigger = Physics2D.OverlapBox(transform.position, bounds, 0f,triggerLayer);
         
        // If camera is in waiting state & the player touches the camera trigger - activate camera.
        if (onTrigger && StateMachine._cameraState == CameraStates.Waiting)
        {
            StateMachine.SetCameraState(CameraStates.Active);
        }
    }
}
