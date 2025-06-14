using System;
using Source.Scripts.Enemies;
using Source.Scripts.Interfaces;
using Source.Scripts.Spawners;
using Source.Scripts.Triggers;
using Source.Scripts.Utilities;
using UnityEngine;

namespace Source.Scripts.Bat
{
    [RequireComponent(typeof(Shooter), typeof(CollisionHandler), typeof(BatAnimator))]
    [RequireComponent(typeof(BatMover))]
    public class Bat : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;

        private BatMover _mover;
        private Shooter _shooter;
        private CollisionHandler _collisionHandler;
        private BatAnimator _animator;
        private bool _isActive;

        public event Action GameOver;

        private void OnValidate()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void Awake()
        {
            _mover = GetComponent<BatMover>();
            _shooter = GetComponent<Shooter>();
            _collisionHandler = GetComponent<CollisionHandler>();
            _animator = GetComponent<BatAnimator>();
        }

        private void OnEnable()
        {
            _collisionHandler.CollisionDetected += ProcessCollision;
            _shooter.Shooting += _animator.Shoot;
            _mover.Swinging += _animator.Swing;
            _inputReader.FlyKeyPressed += TryMove;
            _inputReader.ShootKeyPressed += TryShoot;
        }

        private void OnDisable()
        {
            _collisionHandler.CollisionDetected -= ProcessCollision;
            _shooter.Shooting -= _animator.Shoot;
            _mover.Swinging -= _animator.Swing;
            _inputReader.ShootKeyPressed -= TryShoot;
            _inputReader.FlyKeyPressed -= TryMove;
        }

        private void Update()
        {
            _mover.Fall();
        }

        public void Reset()
        {
            _mover.Reset();
            _animator.Revive();
            _animator.Stand();
            _isActive = true;
        }

        private void TryMove()
        {
            if (_isActive)
            {
                _mover.Move();
            }
        }

        private void TryShoot()
        {
            if (_isActive)
            {
                _shooter.TryShoot();
            }
        }

        private void ProcessCollision(IInteractable interactable)
        {
            if (interactable is Obstacle || interactable is Enemy)
            {
                Die();
            }
            else if (interactable is Projectile projectile && projectile.IsEnemy)
            {
                projectile.CallEvent();
                Die();
            }
        }

        private void Die()
        {
            _isActive = false;
            _animator.Die();
            GameOver?.Invoke();
        }
    }
}