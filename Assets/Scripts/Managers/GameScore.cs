using System;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    [Header("CURRENT SCORE")] [SerializeField]
    private string currentScore;
    
    [Header("SCORE MODIFIERS")] 
    [SerializeField] private int passiveScore = 25;

    [SerializeField] private int levelModifier;

    [Header("KICK BONUS")] [SerializeField]
    private int kickBonus = 100;
    public static Int32 Score;

    private float timer = 0f;
    private float delay = 1.0f;

    private void OnEnable()
    {
        Kick.Flip += OnKick;
        Level.Change += OnLevelChange;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Increase Score Every Second Passively
        if (timer >= delay) {
            timer = 0f;
            Score += passiveScore * levelModifier;
        }
        currentScore = Score.ToString();
    }
    
    private void OnKick()
    {
        Score += kickBonus;
    }

    private void OnLevelChange(int level)
    {
        levelModifier = level;
    }

}
