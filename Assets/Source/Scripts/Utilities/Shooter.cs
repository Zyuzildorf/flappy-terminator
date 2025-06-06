using System;
using System.Collections;
using Source.Scripts.Spawners;
using UnityEngine;

namespace Source.Scripts.Utilities
{
    [RequireComponent(typeof(ProjectileSpawner))]
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private float _attackDelay = 2f;
        [SerializeField] private AudioSource _shootSound;

        private ProjectileSpawner _bulletSpawner;
        private WaitForSeconds _waitForSeconds;
        private bool _isShootDelayOver;
        private bool _isShooting;

        public event Action Shooting;

        private void Awake()
        {
            _bulletSpawner = GetComponent<ProjectileSpawner>();
            _waitForSeconds = new WaitForSeconds(_attackDelay);
            _isShootDelayOver = true;
        }

        private void OnEnable()
        {
            _isShootDelayOver = true;
        }

        private void OnDisable()
        {
            StopCoroutine(WaitForNextAttack());
            _isShootDelayOver = false;
        }

        public void StartShooting()
        {
            _isShooting = true;
            
            StartCoroutine(ShootOverTime());
        }

        public void Shoot()
        {
            if (_isShootDelayOver == false)
                return;

            _bulletSpawner.SpawnBullet();
            _shootSound.Play();
            StartCoroutine(WaitForNextAttack());

            Shooting?.Invoke();
        }

        public void Reset()
        {
            _bulletSpawner.Reset();
        }

        private IEnumerator WaitForNextAttack()
        {
            _isShootDelayOver = false;
            yield return _waitForSeconds;
            _isShootDelayOver = true;
        }

        private IEnumerator ShootOverTime()
        {
            while (_isShooting)
            {
                Shoot();
                yield return _waitForSeconds;
            }
        }
    }
}