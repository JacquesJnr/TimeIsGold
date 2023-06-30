using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameSetup : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private static GameObject Player;

    private static void InstancePlayerObject()
    {
        GameObject spawnedPlayer = Instantiate(Player);
        GamePosition.SetPlayer(spawnedPlayer);
    }

    public static void Setup()
    {
        InstancePlayerObject();
        GameScore.Score = 0;
        Kick.KickCount = 0;
    }
    
    private void Awake()
    {
        Player = player;
    }

    private void OnEnable()
    {
        Setup();
    }

}
