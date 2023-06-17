using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPlatforms : MonoBehaviour
{
    public List<GameObject> normalPlatforms;
    public List<GameObject> spikePlatforms;
    public Transform min, max;
    
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float spawnTimer = 0;

    public Transform spawnPos;
    private List<GameObject> spawned = new List<GameObject>();

    // Select A Random Platfrom Without Spikes To Spawn
    private int platformType;

    // Instance A new Platform at the top of the screen
    public void NewPlatform(GameObject platform)
    {
        int count = spawned.Count;

        float xPos;

        xPos = spawned.Count % 2 == 0 ? platform.transform.position.x : -platform.transform.position.x;
        
        var position = new Vector3(xPos, spawnPos.position.y, 0);
        GameObject newPlatform = Instantiate(platform, position, Quaternion.identity, transform);
        float lastPos = position.y;
        newPlatform.name = ("P" + transform.childCount);
        
        spawned.Add(newPlatform);
        Vector3 newPlatformPos = new Vector3(-newPlatform.transform.position.x, lastPos, 0);
        
        
    }

    private void Update()
    {
        if (spawnTimer < spawnInterval)
        {
            spawnTimer += Time.deltaTime;
        }
        else
        {
            NewPlatform(normalPlatforms[2]);
            spawnTimer = 0;
        }
    }
}
