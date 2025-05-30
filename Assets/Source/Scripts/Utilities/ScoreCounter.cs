using System;
using System.Collections.Generic;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Utilities
{
    public class ScoreCounter : MonoBehaviour
    {
        private List<IScorable> _scorables;

        public float ScoreValue { get; private set; }
        public float BestScoreValue { get; private set; }

        public event Action ScoreValueChanged;

        private void Awake()
        {
            _scorables = new List<IScorable>();
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
}