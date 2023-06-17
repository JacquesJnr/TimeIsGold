using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCamera : MonoBehaviour
{
    [SerializeField] private LayerMask triggerLayer;
    [SerializeField] private bool onTrigger;
    
    // Update is called once per frame
    void Update()
    {
        // Trigger Camera
        onTrigger = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().size, 0f,triggerLayer);
            
        if (onTrigger && StateMachine._cameraState == CameraStates.Waiting)
        {
            StateMachine.SetCameraState(CameraStates.Active);
        }
    }
}
