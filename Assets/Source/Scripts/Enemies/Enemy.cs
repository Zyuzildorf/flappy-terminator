using System;
using Source.Scripts.Interfaces;
using Source.Scripts.Spawners;
using Source.Scripts.Triggers;
using Source.Scripts.Utilities;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    [RequireComponent(typeof(CollisionHandler))]
    public abstract class Enemy : MonoBehaviour, IInteractable, IScorable
    {
        [SerializeField] private int _scoreValue;

        private CollisionHandler _collisionHandler;

        private bool _isActive;

        public event Action<int> GivingScore;
        public event Action<Enemy> Destroying;

        protected virtual void Awake()
        {
            _collisionHandler = GetComponent<CollisionHandler>();
            _isActive = false;
        }

        protected virtual void OnEnable()
        {
            _collisionHandler.CollisionDetected += ProcessCollision;
        }

        protected virtual void OnDisable()
        {
            _collisionHandler.CollisionDetected -= ProcessCollision;
        }

        protected virtual void ProcessCollision(IInteractable interactable)
        {
            if (interactable is Projectile projectile && projectile.IsEnemy == false && _isActive)
            {
                projectile.CallEvent();
                GivingScore?.Invoke(_scoreValue);

                Die();
            }

            if (interactable is ActivationTrigger)
            {
                _isActive = true;
            }

            if (interactable is OutOfScreenTrigger)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroying?.Invoke(this);
            Destroy(gameObject);
        }
    }
}