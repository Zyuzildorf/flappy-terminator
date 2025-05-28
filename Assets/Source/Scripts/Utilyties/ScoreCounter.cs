using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private List<IScorable> _scorables = new List<IScorable>();

    public float ScoreValue { get; private set; }
    public float BestScoreValue { get; private set; }

    public event Action ScoreValueChanged;

    private void Awake()
    {
        ScoreValue = 0;
        BestScoreValue = 0;
    }

    public void AddScorable(List<IScorable> scorables)
    {
        foreach (IScorable scorable in scorables)
        {
            _scorables.Add(scorable);
            scorable.GivingScore += Add;
        }
    }

    public void Reset()
    {
        _scorables.ForEach(scorable => scorable.GivingScore -= Add);
        _scorables.Clear();
        
        if (ScoreValue > BestScoreValue)
        {
            BestScoreValue = ScoreValue;
        }

        ScoreValue = 0;
    }
    
    private void Add(int addScore)
    {
        ScoreValue += addScore;
        ScoreValueChanged?.Invoke();
    }
}
