using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TarodevController;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnPlatforms : MonoBehaviour
{
    public List<GameObject> normalPlatforms;
    public List<GameObject> spikePlatforms;

    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float spawnTimer = 0;
    [SerializeField] private int platformsPerCycle = 5;

    public Transform cutOffHeight;
    private static List<GameObject> spawned = new List<GameObject>();
    [SerializeField] private List<GameObject> platformPool = new List<GameObject>();

    [SerializeField] private GameObject initialPlatform;
    private GameObject previousPlatform { get; set; }

    private void OnEnable()
    {
        Level.Change += OnLevelChange;
    }

    // Instance A new Platform at the top of the screen
    public void NewPlatform(GameObject prefab)
    {
        if (previousPlatform == null) previousPlatform = initialPlatform;
        
        // Set position based on previous platforms Transform
        var xPos = SwappedX();
        var yPos = RandomHeight();
        
        var position = new Vector2(xPos, yPos);
        
        // Initialise Platform using this KICK ASS class I wrote
        Platform p = new Platform("P" + transform.childCount, prefab, position);
        
        // Instance Platform
        Transform asChild = transform;
        GameObject instance = Instantiate(p.platform, asChild);

        // Add instatiated platform to spawned List
        spawned.Add(instance);
        SetPreviousPlatform(instance);
        
    }

    public void SetPreviousPlatform(GameObject obj)
    {
        for (int i = 0; i < spawned.Count; i++)
        {
            if (i > 0)
            {
                previousPlatform = obj;
            }
        }
    }

    public float RandomHeight()
    {
        float r = Random.Range(6f, 10f);
        return previousPlatform.transform.localPosition.y + r;
    }

    public float SwappedX()
    {
        return -previousPlatform.transform.localPosition.x;
    }

    public void DestroyOffScreen()
    {
        if(spawned.Count < 1) return;
        
        // Destroy Platforms Off-screen
        foreach (Transform p in transform)
        {
            if (p.gameObject.CompareTag("Initial Platform"))
            {
                return;
            }

            if (p.position.y < cutOffHeight.position.y)
            {
                //Debug.Log("Popping: " + p.name);
                Destroy(p.gameObject);
            }
        }
    }

    public static void DestroyAllPlatforms()
    {
        if (spawned.Count < 1) { return;}

        foreach (var platform in spawned)
        {
            Destroy(platform);
        }
        spawned.Clear();
    }
    
    public void OnLevelChange(int level)
    {
        platformPool.Clear();
        platformPool = SetLevelPlatforms(level);
    }

    public List<GameObject> SetLevelPlatforms(int level)
    {
        List<GameObject> plastformLengths = new List<GameObject>();
        
        plastformLengths.Clear();
        switch (level)
        {
            case 1:
                plastformLengths.Add(PlatformType("large"));
                break;
            case 2:
                plastformLengths.Add(PlatformType("medium"));
                break;
            case 3:
                plastformLengths.Add(PlatformType("small"));
                break;
        }
        
        return plastformLengths;
    }

    public GameObject PlatformType(string type)
    {
        GameObject stringSearched = null;
        
        foreach (var platform in normalPlatforms)
        {
            if (platform.name.Contains(type))
            {
                stringSearched = platform;
                return stringSearched;
            }
        }
        return stringSearched;
    }

    public GameObject SelectRandomPlatform(List<GameObject> platforms)
    {
        if (platforms.Count == 1)
        {
            return platforms[0];
        }
        else
        {
            int ran = Random.Range(0, platforms.Count);

            return platforms[ran];
        }
    }
    
    private void Update()
    {
        if (StateMachine._cameraState == CameraStates.Waiting)
        {
            //DestroyAll(); 
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0;
            for (int i = 0; i < platformsPerCycle; i++)
            {
                NewPlatform(SelectRandomPlatform(platformPool));
            }
        }
        
        DestroyOffScreen();
        
        #region Manual Overrides

        // Manual Spawn
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            NewPlatform(normalPlatforms[2]);
        }
        
        #endregion
    }
}
