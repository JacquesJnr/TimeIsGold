using System.Collections;
using UnityEngine;

public class EndTransition : MonoBehaviour
{
    [SerializeField] private Transform gameCam;
    private Vector3 camStartPos;
    
    [Header("END GAME LERP")] 
    [SerializeField] private float animationTime;

    private bool resetCamera;
    
    private void Start()
    {
        camStartPos = gameCam.transform.position;
    }
    
    private IEnumerator ResetCamera()
    {
        var elapsedTime = 0f;
        
        while (gameCam.position.y > camStartPos.y)
        {
            gameCam.transform.position = Vector3.Lerp(gameCam.position, camStartPos, elapsedTime / animationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameCam.transform.position = camStartPos;
        EnvrironmentReset();
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
            resetCamera = false;
        }

        if (StateMachine._gameState == GameStates.Menu && !resetCamera)
        {
            // gameCam.transform.LeanMove(camStartPos, animationTime)
            //     .setOnComplete(EnvrironmentReset);
            
            StartCoroutine(ResetCamera());
            resetCamera = true;
        }
    }
}
