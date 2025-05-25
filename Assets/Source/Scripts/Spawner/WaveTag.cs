using System;
using UnityEngine;

public class WaveTag : MonoBehaviour
{
    private Enemy[] _enemies;

    public event Action<WaveTag> AllEnemiesDestroyed;

    private void Awake()
    {
        InitEnemiesArray();
    }

    private void InitEnemiesArray()
    {
        Enemy[] _enemies = this.GetComponentsInChildren<Enemy>();

        foreach (Enemy enemy in _enemies)
        {
            enemy.PrefferedToDestroy += Deactivate;
        }
    }

    private void Reset()
    {
        AllEnemiesDestroyed?.Invoke(this);

        foreach (Enemy enemy in _enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }

    private void Deactivate(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.Reset();

        if (CheckForActiveObjectExistance() == false)
        {
            Reset();
        }
    }

    private bool CheckForActiveObjectExistance()
    {
        foreach (Enemy enemy in _enemies)
        {
            if (enemy.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
}