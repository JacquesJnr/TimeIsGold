using System;
using UnityEngine;
using UnityEngine.UI;

public class StartingTransition : MonoBehaviour
{
    [SerializeField] private CanvasGroup controlls;
    [SerializeField] private GameObject hourglass;

    private void OnEnable()
    {
        Kick.Flip += FadeOutControls;
    }

    public void FadeOutControls()
    {
        if (Kick.KickCount > 1) { return;}
        controlls.LeanAlpha(0, 1);
    }

    private void Update()
    {
       
    }
}
