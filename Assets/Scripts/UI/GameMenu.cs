using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
   [SerializeField] private CanvasGroup credits;
   [SerializeField] private float fadeTime;

   public void ShowCredits()
   {
      credits.LeanAlpha(1, fadeTime).setOnComplete(() => credits.blocksRaycasts = true);
   }
   
   public void HideCredits()
   {
      credits.LeanAlpha(0, fadeTime).setOnComplete(() => credits.blocksRaycasts = false);
   }

   public void StartGame()
   {
      SceneManager.LoadScene("Time Is Gold");
   }
}