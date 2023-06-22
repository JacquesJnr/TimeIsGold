using UnityEngine;

public class KickTrigger : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public static bool WithinKickRange;
    [SerializeField] private bool inRange;
    [SerializeField] private BoxCollider2D hourglassHitBox;

    private void FixedUpdate()
    {
        Vector2 hitBoxBounds = hourglassHitBox.size;
        WithinKickRange = Physics2D.OverlapBox(transform.position, hitBoxBounds, 0f, whatIsPlayer);
        inRange = WithinKickRange;
    }
}