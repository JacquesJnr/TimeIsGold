using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessCamera : MonoBehaviour
{ 
    [Header("CAMERA OBJECT")]
    [SerializeField] private GameObject cam;
    
    [Header("CAMERA SPEED")]
    [SerializeField] private float cameraSpeed;
    
    [Header("MAX SPEED - Increase at your own risk")]
    [SerializeField] float maxSpeed = 10f;
    
    [Header("SPEED/DISTANCE INCREASE OVER TIME")]
    [SerializeField] private float speedIncrease = 0.5f;
    [SerializeField] private float distanceIncrease= 60f;

    [Header("TARGET HEIGHT")] 
    [SerializeField] private float switchHeight = 10f;
    
    
    
    public static float CameraSpeed;
    public static float CameraHeight;

    private void Start()
    {
        // Initialise Camera Speed
        CameraSpeed = 1;
    }

    private void Update()
    {
        cameraSpeed = CameraSpeed;
        
       if (StateMachine._cameraState == CameraStates.Active)
       {
           cam.transform.Translate(0, cameraSpeed * Time.deltaTime, 0);
       }
       else
       {
           cam.transform.Translate(Vector3.zero);
       }

       CameraHeight = cam.transform.position.y;
       
       if (CameraSpeed < maxSpeed)
       {
           if (CameraHeight > switchHeight)
           {
               CameraSpeed += speedIncrease;
               switchHeight += distanceIncrease;
           }
       }
    }
}
