using System;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public abstract class Enemy : MonoBehaviour, IInteractable
{
    protected CollisionHandler _collisionHandler;
    //private MoverRightLeft _mover;
    //private MoverSinWave _sinWaveMover;
    //private Shooter _shooter;
    protected Vector2 _startPosition;
    private bool _isActive = false;
    private bool _isOnShootPosition = false;

    protected event Action<Enemy> PrefferedToDestroy;

    protected virtual void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        //_mover = GetComponent<MoverRightLeft>();
        //_sinWaveMover = GetComponent<MoverSinWave>();
        //_shooter = GetComponent<Shooter>();
        _startPosition = transform.position;
    }

    protected virtual void OnEnable()
    {
        _collisionHandler.CollisionDetected += ProcessCollision;
        _collisionHandler.ProjectileDetected += ProccesProjectileCollision;
    }

    protected virtual void OnDisable()
    {
        _collisionHandler.CollisionDetected -= ProcessCollision;
        _collisionHandler.ProjectileDetected -= ProccesProjectileCollision;
    }

    protected virtual void Update()
    {
        Move();

        if (_isOnShootPosition)
        {
            Shoot();
        }
        //_mover.Move();                      // Заглушка движения.
        //_mover.MoveTowardsWaypoint();     // Перенести в дочерний класс конкретных типов противников.
        //_sinWaveMover.MoveSinWave();        // Сделать абстрактный метод Move.



        //_shooter.StartShooting();

    }

    public virtual void Reset()
    {
        transform.position = _startPosition;
    }

    protected abstract void Move();

    protected abstract void Shoot();

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is ActivationTrigger)
        {
            _isActive = true;
        }

        if (interactable is ShootActivationTrigger)
        {
            _isOnShootPosition = true;
        }
    }

    protected virtual void ProccesProjectileCollision(Projectile projectile)
    {
        if (projectile.IsEnemy == false && _isActive)
        {
            projectile.CallEvent();

            PrefferedToDestroy?.Invoke(this);
            Destroy(gameObject);              // Временная заглушка
        }
    }
}
