using UnityEngine;


public class Die : MonoBehaviour
{
    public bool inDeathZone;
    public LayerMask whatIsPlayer;
    
    private void Update()
    {
        inDeathZone = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().size, 0f,whatIsPlayer);

        var Alive = GameStates.Alive;
        
        if (inDeathZone && StateMachine._gameState == Alive)
        { 
            StateMachine.SetPlayerState(GameStates.Dead);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().size);
    }
}
