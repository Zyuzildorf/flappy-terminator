using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<WaveObjectPool> _waves;
    [SerializeField] private float _spawnInterval;

    private bool _isActive = true;

    private void Start()
    {
        Spawn();
        StartSpawning();
    }

    public void StartSpawning()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (_isActive)
        {
            yield return new WaitForSeconds(_spawnInterval);
            Spawn();
        }
    }

    private void Spawn()
    {
        WaveObjectPool pool = _waves[Random.Range(0, _waves.Count)];
        pool.GetObject().gameObject.transform.position = _spawnPoint.position;
    }
}