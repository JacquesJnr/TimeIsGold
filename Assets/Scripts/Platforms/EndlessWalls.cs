using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessWalls : MonoBehaviour
{
   public SpriteRenderer left, right;
   public Transform targetHeight;

   private void Update()
   {
      float endlessHeight = targetHeight.position.y;
      
      left.size = new Vector2(1, endlessHeight);
      right.size = new Vector2(1, endlessHeight);
   }
}
