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
    private Coroutine _currentCoroutine;
    private bool _isActive = true;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_spawnInterval);
        _spawnedWaveObjects = new List<WaveObject>();
    }

    public void Reset()
    {
        if(_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
        
        foreach (WaveObject waveObject in _spawnedWaveObjects)
        {
            Destroy(waveObject.gameObject);
        }

        _spawnedWaveObjects.Clear();

        _currentCoroutine = StartCoroutine(Spawning());
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
        _spawnedWaveObjects.Add(Instantiate(_waves[Random.Range(0, _waves.Count)], _container, true));
        WaveObject waveObject = _spawnedWaveObjects[_spawnedWaveObjects.Count - 1];
        waveObject.transform.position = _spawnPoint.position;
        waveObject.WaveDestroying += OnWaveObjectDestroy;
    }

    private void OnWaveObjectDestroy(WaveObject waveObject)
    {
        waveObject.WaveDestroying -= OnWaveObjectDestroy;
        
        _spawnedWaveObjects.Remove(waveObject);
    }
}