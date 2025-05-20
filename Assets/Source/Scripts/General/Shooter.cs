using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ProjectileSpawner))]
public class Shooter : MonoBehaviour
{
    [SerializeField] private float _attackDelay = 2f;

    private ProjectileSpawner _bulletSpawner;
    private WaitForSeconds _waitForSeconds;
    private bool _isShootDelayOver;

    public event Action Shooting;

    private void Awake()
    {
        _bulletSpawner = GetComponent<ProjectileSpawner>();
        _waitForSeconds = new WaitForSeconds(_attackDelay);
        _isShootDelayOver = true;
    }

    public void StartShooting()
    {
        InvokeRepeating("Shoot", 0f, _attackDelay);
    }

    public void Shoot()
    {
        if (_isShootDelayOver == false)
            return;

        _bulletSpawner.SpawnBullet();
        StartCoroutine(WaitForNextAttack());

        Shooting?.Invoke();
    }

    private IEnumerator WaitForNextAttack()
    {
        _isShootDelayOver = false;
        yield return _waitForSeconds;
        _isShootDelayOver = true;
    }
}
