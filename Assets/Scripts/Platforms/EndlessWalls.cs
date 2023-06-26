using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EndlessWalls : MonoBehaviour
{
   public SpriteRenderer left, right;
   public Transform targetHeight;

   private void Update()
   {
      // Set Sprite size
      float endlessHeight = targetHeight.position.y;
      SetWallHeight(endlessHeight);
      
      // Set Collider size / offset
      UpdateWallColliders();
   }

   private void SetWallHeight(float target)
   {
      left.size = new Vector2(1, target);
      right.size = new Vector2(1, target);
   }

   private void UpdateWallColliders()
   {
      var colRight = right.GetComponent<CapsuleCollider2D>();
      var colLeft = left.GetComponent<CapsuleCollider2D>();

      float leftHeight = left.size.y;
      colLeft.size = new Vector2(1, leftHeight);
      colLeft.offset = new Vector2(0, leftHeight / 2);

      float rightHeight = right.size.y;
      colRight.size = new Vector2(1, rightHeight);
      colRight.offset = new Vector2(0, rightHeight / 2);

   }
}
