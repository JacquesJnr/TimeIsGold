using System;
using UnityEngine;
using UnityEngine.Serialization;

public class KickTrigger : MonoBehaviour
{
    [Header("LAYER CHECK")]
    [SerializeField] private LayerMask kickLayer;
    [Header("IS PLAYER WITHIN KICK RANGE?")]
    [SerializeField] private bool inRange;
    [Header("2D BOX COLLIDER")]
    [SerializeField] private BoxCollider2D hourglassHitBox;
    
    public static bool WithinKickRange;

    private void FixedUpdate()
    {
        Vector2 hitBoxBounds = hourglassHitBox.bounds.size;
        WithinKickRange = Physics2D.OverlapBox(transform.position, hitBoxBounds, 0f, kickLayer);
        inRange = WithinKickRange;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, hourglassHitBox.bounds.size);
    }
}