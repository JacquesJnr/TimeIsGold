using UnityEngine;


public class DeathZone : MonoBehaviour
{
    public bool inDeathZone;
    public LayerMask whatIsPlayer;
    
    private void Update()
    {
        inDeathZone = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().size, 0f,whatIsPlayer);

        var Alive = StateMachine._gameState == GameStates.Playing;
        
        if (inDeathZone && Alive)
        { 
            StateMachine.SetPlayerState(GameStates.Menu);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().size);
    }
}
