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

        public void TryShoot()
        {
            if (_isShootDelayOver == false)
                return;

            Shoot();
            StartCoroutine(WaitForNextAttack());
        }

        public void Reset()
        {
            _bulletSpawner.Reset();
        }
        
        private void Shoot()
        {
            _bulletSpawner.SpawnBullet();
            _shootSound.Play();

            Shooting?.Invoke();
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