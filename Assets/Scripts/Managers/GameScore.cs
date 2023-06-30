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

    private UIScore uiScore;

    private void OnEnable()
    {
        uiScore = FindObjectOfType<UIScore>();
        Kick.Flip += OnKick;
        Level.Change += OnLevelChange;
    }

    private void Update()
    {
        var hourglassIsDraining = StateMachine._hourglassState == HourglassStates.Draining;
        
        // Manually increase score for testing
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnKick();
        }
        
        if(!hourglassIsDraining){return;}
        
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

        Vector3 UpScaled = new Vector2(1.2f, 1.2f);
        uiScore.scoreObject.LeanScale(UpScaled, 0.5f).setLoopPingPong(1);
    }


    private void OnLevelChange(int level)
    {
        levelModifier = level;
    }

}
