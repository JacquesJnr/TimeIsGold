using System.Collections;
using UnityEngine;

public class EndTransition : MonoBehaviour
{
    [SerializeField] private Transform gameCam;
    private Vector3 camStartPos;
    
    [Header("END GAME LERP")] 
    [SerializeField] private float animationTime;
    
    private void Start()
    {
        camStartPos = gameCam.transform.position;
    }
    
    private IEnumerator ResetCamera()
    {
        
        yield return null;
    }

    private void EnvrironmentReset()
    {
        animationTime = 0;
        SpawnPlatforms.DestroyAllPlatforms();
    }
    
    private void Update()
    {
        if (StateMachine._cameraState == CameraStates.Active)
        {
            animationTime = GamePosition.CurrentPlayerHeight / Time.time;
        }

        if (StateMachine._gameState == GameStates.Menu)
        {
            // gameCam.transform.LeanMove(camStartPos, animationTime)
            //     .setOnComplete(EnvrironmentReset);
            
            // TODO: Lerp this
            gameCam.transform.position = camStartPos;
        }
    }
}
