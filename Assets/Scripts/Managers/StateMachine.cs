using UnityEngine;

public enum GameStates
{
    Playing,
    Menu
}

public enum CameraStates
{
    Waiting,
    Active
}

public enum HourglassStates
{
    Full,
    Draining,
    Empty
}

public class StateMachine : MonoBehaviour
{
    public static GameStates _gameState;
    [Header("PLAYER")]
    [SerializeField] private GameStates gameState;
    public GameObject player;
    
    public static CameraStates _cameraState;
    [Header("CAMERA")]
    [SerializeField] private CameraStates cameraState;
    
    public static HourglassStates _hourglassState;
    [Header("HOURGLASS")] 
    [SerializeField] private HourglassStates hourglassState;

    private void Start()
    {
        _gameState = gameState;
        _cameraState = cameraState;
        _hourglassState = hourglassState;

    }
    
    public static void SetPlayerState(GameStates newState)
    {
        _gameState = newState;
    }

    public static void SetCameraState(CameraStates newState)
    {
        _cameraState = newState;
    }

    public static void SetHourglassState(HourglassStates newState)
    {
        _hourglassState = newState;
    }

    private void StopGameState()
    {
        //Time.timeScale = 0;
        SetCameraState(CameraStates.Waiting);
        SetHourglassState(HourglassStates.Full);
    }

    private bool HourglassIsEmpty()
    {
        return _hourglassState == HourglassStates.Empty;
    }

    private bool InMenu()
    {
        return _gameState == GameStates.Menu;
    }

    private void Update()
    {
        if (HourglassIsEmpty() || InMenu())
        {
            StopGameState();
        }

        // Manual Override
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetCameraState(CameraStates.Active);
        }

        hourglassState = _hourglassState;
        cameraState = _cameraState;
        gameState = _gameState;
    }
}
