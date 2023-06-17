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

    [Header("BUTTONS")][SerializeField]
    private SpriteRenderer flipTrigger;

    public static event Action Flip;

    void Awake() => _player = GetComponentInParent<IPlayerController>();
    
    private void Start()
    {
        // Init Collider bounds
        box.size = kickBounds.size;
        box.offset = kickBounds.center;
    }

    private void FixedUpdate()
    {
        CheckForKicks(flipTrigger);
    }

    public void CheckForKicks(SpriteRenderer obj)
    {
        if (!_player.KickingThisFrame)
        {
            return;
        }
        
        if (box.bounds.Intersects(obj.bounds))
        {
            Debug.Log("Kicked " + obj.gameObject.name);
            Flip!.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + kickBounds.center,kickBounds.size);
        }
    }
}
