using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessUp : MonoBehaviour
{
   public float cameraSpeed;
   public GameObject cam;

   private void Update()
   {
       if (StateMachine._cameraState == CameraStates.Active)
       {
           cam.transform.Translate(0, cameraSpeed * Time.deltaTime, 0);
       }
       else
       {
           cam.transform.Translate(Vector3.zero);
       }
   }

   public void GetFaster()
   {
       // Get faster when as the player gets higher up
   }
}
