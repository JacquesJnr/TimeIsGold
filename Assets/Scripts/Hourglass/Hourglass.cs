using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hourglass : MonoBehaviour
{
   [Header("SPRITE RENDERERS")] 
   [SerializeField] private SpriteRenderer top;
   [SerializeField] private SpriteRenderer bottom;
   
   [Header("SPRITES")]
   [SerializeField] private Sprite topFill;
   private Vector2 topPos;
   
   [SerializeField] private Sprite bottomFill;
   private Vector2 bottomPos;
   
   [Header("LERP POSITIONS")]
   public float fillLevel;
   public float emptyLevel;
   private Vector2 full;
   private Vector2 empty;
   [SerializeField] private bool moving;

   [SerializeField] private float sineSpeed;
   [SerializeField] private float sineMag;
   

   [Header("ANIMATION TIME")] 
   [SerializeField] private float animationTime;

   private void Awake()
   {
     //topFill = Resources.Load<Sprite>("Hourglass/sand_top");
     topPos = top.transform.localPosition;
     
     //bottomFill = Resources.Load<Sprite>("Hourglass/sand_bottom");
     bottomPos = bottom.transform.localPosition;
   }

   private void OnEnable()
   {
      Kick.Flip += OnKick;

      empty = new Vector2(1, emptyLevel);
      full = new Vector2(1, fillLevel);
      
      ResetHourglass();
   }

   private float GetOrientation()
   {
      return gameObject.transform.rotation.z;
   }
   
   private void OnKick()
   {
      StateMachine.SetHourglassState(HourglassStates.Draining);
     
      // Top sand relative to hourglass rotation
      SpriteRenderer currentTop = GetOrientation() < 0 ? bottom : top;
      // Bottom sand relative to hourglass rotation
      SpriteRenderer currentBottom = GetOrientation() >= 0 ? bottom : top; 
      
      // Initial kick
      if (Kick.KickCount <= 1)
      {
         currentTop = bottom;
         currentBottom = top;
      }
      
      // Set the top sands sprite and position
      EvaluateSprite(currentTop, topPos, topFill);
      
      // Set the bottom sands sprite and position
      EvaluateSprite(currentBottom, bottomPos, bottomFill);

      StopAllCoroutines();
      StartCoroutine(Flow(currentBottom, full));
      StartCoroutine(Flow(currentTop, empty));
   }

   public void EvaluateSprite(SpriteRenderer sp, Vector2 targetPos, Sprite newSprite)
   {
      sp.transform.localPosition = targetPos;
      sp.sprite = newSprite;
   }

   IEnumerator Flow(SpriteRenderer sp, Vector2 target)
   {
      float elapsed = 0f;
      Vector2 valueToLerp;

      while (elapsed < animationTime)
      {
         valueToLerp = Vector2.Lerp(sp.size, target, elapsed / animationTime);
         sp.size = valueToLerp;
         elapsed += Time.deltaTime;
         yield return null;
      }
      valueToLerp = target;
   }

   public void ResetHourglass()
   {
      EvaluateSprite(top, topPos, topFill);
      top.size = empty;
      EvaluateSprite(bottom, bottomPos, bottomFill);
      bottom.size = full;
   }

   public void Move()
   {
      Vector3 newPosition = new Vector3(0, SineAmount(), 10);
      transform.localPosition = newPosition;
   }

   private float SineAmount()
   {
      return sineMag * MathF.Sin(Time.time * sineSpeed);
   }

   public void PingPongHourglass()
   {
      float magnitude = 0.5f;
      float pingPong = magnitude * MathF.Sin(Time.time);

      transform.localPosition = new Vector3(0, pingPong, 10);
   }


   private void Update()
   {
      if (StateMachine._hourglassState == HourglassStates.Full)
      {
         ResetHourglass();
         PingPongHourglass();
      }

      if (StateMachine._hourglassState == HourglassStates.Draining)
      {
         Move();
      }
   }
}
