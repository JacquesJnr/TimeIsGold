using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIScoreMenu : MonoBehaviour
{
   [Header("WINDOW RECT TRNASFORM")]
   [SerializeField] private RectTransform restartWindow;
   [Header("OPEN / CLOSE ANIMATION SPEED")] 
   [SerializeField] private float animationTime;

   [SerializeField] private TMP_Text highScore;
   [SerializeField] private TMP_Text score;

    private void OnEnable()
   {
      StateMachine.OnGameEnd += Open;
   }

   private void Open()
   {
      gameObject.SetActive(true);
      
      highScore.text = GameScore.HighScore.ToString();
      score.text = GameScore.Score.ToString();
      
      restartWindow.LeanScale(Vector3.one, animationTime).setEase(LeanTweenType.easeSpring);
      
   }

   public void Restart()
   {
      StateMachine.SetPlayerState(GameStates.Playing);
      GameSetup.Setup();
      
      restartWindow.LeanScale(Vector3.zero, animationTime).setOnComplete(() => gameObject.SetActive(false));
   }
}
