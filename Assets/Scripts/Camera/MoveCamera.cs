using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessUp : MonoBehaviour
{ 
    [SerializeField] private float cameraSpeed; 
    [SerializeField] private GameObject cam;

    public static float CameraSpeed { get; set; }

    private void Update()
    {
        //cameraSpeed = CameraSpeed;
        
       if (StateMachine._cameraState == CameraStates.Active)
       {
           cam.transform.Translate(0, cameraSpeed * Time.deltaTime, 0);
       }
       else
       {
           cam.transform.Translate(Vector3.zero);
       }
   }
}
