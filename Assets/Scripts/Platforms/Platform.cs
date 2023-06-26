using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Platform 
{ 
    public string name;
    public Vector2 position;
    public GameObject platform { get; set; }

    public Platform(string platformName, GameObject platformType, Vector2 platformPosition) 
    {
        name = platformName;
        position = platformPosition;
        platform = platformType;
        InitNewPlatform();
    }
    public void InitNewPlatform()
    {
        platform.transform.position = position;
    }

    public void Destroy()
    {
        Object.Destroy(platform);
    }
}
