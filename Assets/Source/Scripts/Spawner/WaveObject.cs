using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveObject : MonoBehaviour
{
    private List<Enemy> _enemies;

    public event Action<WaveObject> WaveDestroying;
    
    private void Awake()
    {
        InitEnemiesArray();
    }

    private void InitEnemiesArray()
    {
        _enemies = GetComponentsInChildren<Enemy>().ToList();

        foreach (Enemy enemy in _enemies)
        {
            enemy.Destroing += CheckEnemiesRemaining;
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
}