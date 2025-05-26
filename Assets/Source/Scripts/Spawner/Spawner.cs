using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _container;
    [SerializeField] private List<WaveObject> _waves;
    [SerializeField] private float _spawnInterval;

    private List<WaveObject> _spawnedWaveObjects;
    private WaitForSeconds _waitForSeconds;
    private bool _isActive;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_spawnInterval);
        _spawnedWaveObjects = new List<WaveObject>();
    }

    public void StartSpawning()
    {
        _isActive = true;
        StartCoroutine(Spawning());
    }

    public void Reset()
    {
        _isActive = false;
        StopCoroutine(Spawning());

        foreach (WaveObject waveObject in _spawnedWaveObjects)
        {
            Destroy(waveObject);
        }
        
        _spawnedWaveObjects.Clear();
    }

    private IEnumerator Spawning()
    {
        while (_isActive)
        {
            Spawn();
            yield return _waitForSeconds;
        }
    }

    private void Spawn()
    {
        WaveObject wave = Instantiate(_waves[Random.Range(0, _waves.Count)], _container, true);
        wave.transform.position = _spawnPoint.position;
        
        _spawnedWaveObjects.Add(wave);
    }
}