using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(BatMover), typeof(Shooter))]
[RequireComponent (typeof(BatScoreCounter),typeof(CollisionHandler), typeof(BatAnimator))]
public class Bat : MonoBehaviour
{
    private InputReader _inputReader;
    private BatMover _mover;
    private Shooter _attacker;
    private BatScoreCounter _scoreCounter;
    private CollisionHandler _collisionHandler;
    private BatAnimator _animator;

    public event Action GameOver;

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<BatMover>();
        _attacker = GetComponent<Shooter>();
        _scoreCounter = GetComponent<BatScoreCounter>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _animator = GetComponent<BatAnimator>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += ProcessCollision;
        _collisionHandler.ProjectileDetected += ProcessProjectileCollision;
        _attacker.Shooting += _animator.Shoot;
        _mover.Swinging += _animator.Swing;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= ProcessCollision;
        _collisionHandler.ProjectileDetected -= ProcessProjectileCollision;
        _attacker.Shooting -= _animator.Shoot;
        _mover.Swinging -= _animator.Swing;
    }

    private void Update()
    {
        if(_inputReader.IsSpacebarPressed)
        {
            _mover.Move();
        }

        if(_inputReader.IsFKeyPressed)
        {
            _attacker.Shoot();
        }

        _mover.Fall();
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Obstacle)
        {
            _animator.Die();
            GameOver?.Invoke();
        }        

        if(interactable is Enemy)
        {
            _animator.Die();
            GameOver?.Invoke();
        }
    }

    private void ProcessProjectileCollision(Projectile projectile)
    {
        if (projectile.IsEnemy)
        {
            projectile.CallEvent();

            _animator.Die();
            GameOver?.Invoke();
        }
    }

    public void Reset()
    {
        _mover.ResetPosition();
        _scoreCounter.ResetScore();
        _animator.Revive();
        _animator.Stand();
    }
}
