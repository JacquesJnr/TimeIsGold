using System;
using UnityEngine;
using UnityEngine.UI;

public class StartingTransition : MonoBehaviour
{
    [SerializeField] private GameObject startCam;
    [SerializeField] private GameObject gameCam;
    [SerializeField] private CanvasGroup controlls;

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
        if (StateMachine._cameraState == CameraStates.Active && Kick.KickCount == 1)
        {
            startCam.SetActive(false);
        }
    }
}
