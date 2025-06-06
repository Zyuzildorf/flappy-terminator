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

        public event Action<WaveObject> WaveDestroying;

        public List<IScorable> ScorableEnemies => GetScorables();

        private void Awake()
        {
            InitEnemiesList();
        }

        public void Reset()
        {
            foreach (Enemy enemy in _enemies)
            {
                if (enemy.TryGetComponent(out EnemyCupid cupid))
                {
                    cupid.Reset();
                }
                else if (enemy.TryGetComponent(out EnemyWalker walker))
                {
                    walker.Reset();
                }
            }
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
            enemy.Destroying -= CheckEnemiesRemaining;
            
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