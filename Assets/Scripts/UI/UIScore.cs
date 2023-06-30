using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [SerializeField] private int lerpingScore;
    [SerializeField] private TMP_Text displayScore;
    public GameObject scoreObject;
    [SerializeField] private int lerpSpeed;

    private void Start()
    {
        scoreObject = displayScore.gameObject;
    }

    private void DisplayScoreOnUI()
    {
        if (lerpingScore < GameScore.Score)
        {
            lerpingScore += 1 * lerpSpeed;
            displayScore.text = lerpingScore.ToString();
        }
    }

    public void ResetScore()
    {
        lerpingScore = 0;
        displayScore.text = "0";
    }

    private void Update()
    {
        DisplayScoreOnUI();
    }
}
