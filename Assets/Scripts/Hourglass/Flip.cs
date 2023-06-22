using System;
using UnityEngine;
using System.Threading.Tasks;

public class Flip : MonoBehaviour
{
    public float animationTime;

    private void OnEnable()
    {
        Kick.Flip += OnKick;
    }

    private Vector3 NewRotation(Quaternion newRotation)
    {
        var flipIt = newRotation.eulerAngles;
        flipIt.z -= 180f;

        return flipIt;
    }

    public void OnKick()
    {
        LeanTween.cancel(gameObject);
        var zRotation = gameObject.transform.rotation;
        var endPos = NewRotation(zRotation);
        LeanTween.rotate(gameObject,endPos, animationTime).
            setEase(LeanTweenType.easeOutElastic);
    }
}