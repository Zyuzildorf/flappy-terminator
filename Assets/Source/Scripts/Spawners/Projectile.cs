using System;
using System.Collections;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Spawners
{
    public class Projectile : MonoBehaviour, IInteractable
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime = 2;
        [SerializeField] private bool _isEnemy = false;

        private WaitForSeconds _waitForSeconds;

        public event Action<Projectile> PrefferToDestroyed;
        public bool IsEnemy => _isEnemy;

        private void OnValidate()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(_lifeTime);
        }

        private void OnDisable()
        {
            PrefferToDestroyed?.Invoke(this);
        }

        private void OnDestroy()
        {
            PrefferToDestroyed = null;
        }

        public void SetVelocity(Vector2 direction)
        {
            _rigidbody.velocity = direction * _speed;
        }

        public void StartLifeTimeDecreasing()
        {
            StartCoroutine(DecreaseLifeTime());
        }

        public void CallEvent()
        {
            PrefferToDestroyed?.Invoke(this);
        }

        private IEnumerator DecreaseLifeTime()
        {
            yield return _waitForSeconds;
            
            Die();
        }

        private void Die()
        {
            PrefferToDestroyed?.Invoke(this);
            _rigidbody.velocity = Vector2.zero;
        }
    }
}