using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EmptyCheck : MonoBehaviour
{
    private SpriteRenderer sp;
    private bool empty;
    private float magicNumber = 0.009f;

    private void Awake()
    {
        sp = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (StateMachine._hourglassState != HourglassStates.Draining) return;
        if(sp.transform.localPosition.y > 0) return;

        if (Kick.KickCount == 1)
        {
            
        }
        
        if (sp.size.y < magicNumber && !empty)
        {
            empty = true;
            StateMachine.SetHourglassState(HourglassStates.Empty);
        }
    }
}
