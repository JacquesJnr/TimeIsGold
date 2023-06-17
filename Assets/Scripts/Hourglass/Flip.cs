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

    public async void OnKick()
    {
        float from = gameObject.transform.rotation.z;
        float to = (gameObject.transform.rotation.z - 180);
        float timeElapsed = 0;  

        float lerpValue;
        while (timeElapsed < animationTime)
        {
            lerpValue = Mathf.Lerp(from, to, timeElapsed / animationTime);
            timeElapsed += Time.deltaTime;
            
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, lerpValue));
            
            await Task.Yield();
        }
        
        gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, to));
    }
}