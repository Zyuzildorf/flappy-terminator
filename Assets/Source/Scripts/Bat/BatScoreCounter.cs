using System;
using UnityEngine;

public class BatScoreCounter : MonoBehaviour
{
    public int ScoreValue {  get; private set; }
    public int BestScoreValue { get; private set; }

    public event Action ScoreValueChanged;

    private void Awake()
    {
        ScoreValue = 0;
        BestScoreValue = 0;
    }

    public void Add()
    {
        ScoreValue++;
        ScoreValueChanged?.Invoke();
    }

    public void ResetScore()
    {
        if(ScoreValue > BestScoreValue)
        {
            BestScoreValue = ScoreValue;
        }

        ScoreValue = 0;
    }
}
