using UnityEngine;

public class GamePosition : MonoBehaviour
{
    [Header("HEIGHT")]
    [SerializeField] private string currentHeight = "0.00";
    private Vector2 pos;
    public static float CurrentPlayerHeight { get; set; }
    
    [Header("TRANSFORMS")]
    private static Transform Player;
    [SerializeField] private Transform floor;

    [Header("EXTENT OFFSET")]
    [SerializeField] float playerExtents;
    
    private bool NoPlayerAssigned()
    {
        return Player == null;
    }

    public static void SetPlayer(GameObject newPlayer)
    {
        Player = newPlayer.transform;
    }
    
    private void Update()
    {
        if(StateMachine._gameState == GameStates.Menu){return;}
        if (NoPlayerAssigned()) { return;}

        pos = Player.position;
        var relativePosition = (pos.y - playerExtents) - floor.position.y;
        currentHeight = relativePosition.ToString("F2");
        CurrentPlayerHeight = relativePosition;
    }
    
}
