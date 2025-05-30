using System;
using System.Collections.Generic;
using System.Linq;
using Source.Scripts.Enemies;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Spawners
{
    public class WaveObject : MonoBehaviour
    {
        private List<Enemy> _enemies;

        public List<IScorable> ScorableEnemies => GetScorables();
        public event Action<WaveObject> WaveDestroying;
    
        private void Awake()
        {
            InitEnemiesList();
        }

        private void InitEnemiesList()
        {
            _enemies = GetComponentsInChildren<Enemy>().ToList();

            foreach (Enemy enemy in _enemies)
            {
                enemy.Destroying += CheckEnemiesRemaining;
            }
        }

        private void CheckEnemiesRemaining(Enemy enemy)
        {
            _enemies.Remove(enemy);
        
            if (_enemies.Count <= 0)
            {
                WaveDestroying?.Invoke(this);
                Destroy(gameObject);
            }
        }

        private List<IScorable> GetScorables()
        {
            List<IScorable> scorables = new List<IScorable>(_enemies);

            return scorables;
        }
    }
}