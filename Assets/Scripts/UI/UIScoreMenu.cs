using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreMenu : MonoBehaviour
{
   [SerializeField] private RectTransform restartWindow;
   [SerializeField] private float animationTime;

    private void OnEnable()
   {
      StateMachine.OnGameEnd += Open;
   }

   private void Open()
   {
      gameObject.SetActive(true);
      restartWindow.LeanScale(Vector3.one, animationTime).setEase(LeanTweenType.easeSpring);
   }

   public void Restart()
   {
      StateMachine.SetPlayerState(GameStates.Playing);
      GameSetup.Setup();
      restartWindow.LeanScale(Vector3.zero, animationTime).setOnComplete(() => gameObject.SetActive(false));
   }
}
