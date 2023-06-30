using System;
using TarodevController;
using UnityEngine;

public class Kick : MonoBehaviour
{
    [SerializeField] private Bounds kickBounds;
    private BoxCollider2D box {
        get { return GetComponent<BoxCollider2D>(); }
    }
    private IPlayerController _player;

    public static event Action Flip;
    
    [SerializeField] private int kickCount;
    public static int KickCount;

    void Awake() => _player = GetComponentInParent<IPlayerController>();
    
    private void Start()
    {
        // Init Collider bounds
        box.size = kickBounds.size;
        box.offset = kickBounds.center;
    }

    private void FixedUpdate()
    {
        CheckForKicks();
        kickCount = KickCount;
    }

    public void CheckForKicks()
    {
        if (!KickTrigger.WithinKickRange)
        {
            return;
        }

        if (_player.KickingThisFrame)
        {
            KickCount++;
            Flip!.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isEditor)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + kickBounds.center,kickBounds.size);
        }
    }
}
