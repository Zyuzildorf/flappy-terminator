using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private List<IScorable> _scorables = new List<IScorable>();

    public int ScoreValue { get; private set; }
    public int BestScoreValue { get; private set; }

    public event Action ScoreValueChanged;

    private void Awake()
    {
        ScoreValue = 0;
        BestScoreValue = 0;
    }

    public void AddScorable(IScorable scorable)
    {
        _scorables.Add(scorable);

        scorable.OnScoreValueAdd += Add;
    }

    private void OnDestroy()
    {
        _scorables.ForEach(scorable => scorable.OnScoreValueAdd -= Add);
    }

    public void Add(int addScore)
    {
        ScoreValue += addScore;
        ScoreValueChanged?.Invoke();
    }

    public void ResetScore()
    {
        if (ScoreValue > BestScoreValue)
        {
            BestScoreValue = ScoreValue;
        }

        ScoreValue = 0;
    }
}
