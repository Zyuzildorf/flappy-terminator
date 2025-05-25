using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(BatMover), typeof(Shooter))]
[RequireComponent (typeof(ScoreCounter),typeof(CollisionHandler), typeof(BatAnimator))]
public class Bat : MonoBehaviour
{
    private InputReader _inputReader;
    private BatMover _mover;
    private Shooter _attacker;
    private ScoreCounter _scoreCounter;
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
        _scoreCounter = GetComponent<ScoreCounter>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _animator = GetComponent<BatAnimator>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += ProcessCollision;
        _attacker.Shooting += _animator.Shoot;
        _mover.Swinging += _animator.Swing;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= ProcessCollision;
        _attacker.Shooting -= _animator.Shoot;
        _mover.Swinging -= _animator.Swing;
    }

    private void Update()
    {
        if(_inputReader.IsKeyFPressed)
        {
            _mover.Move();
        }

        if(_inputReader.IsKeyKPressed)
        {
            _attacker.Shoot();
        }

        _mover.Fall();
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
        _animator.Die();
        GameOver?.Invoke();
    }
    
    public void Reset()
    {
        _mover.Reset();
        _scoreCounter.ResetScore();
        _animator.Revive();
        _animator.Stand();
    }
}