using System;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public abstract class Enemy : MonoBehaviour, IInteractable, IScorable
{
    [SerializeField] private int _scoreValue;

    private CollisionHandler _collisionHandler;

    private Vector2 _startPosition;
    private bool _isActive = false;

    public event Action<int> GivingScore;
    public event Action<Enemy> Destroing;

    protected virtual void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        _startPosition = transform.localPosition;
    }

    protected virtual void OnEnable()
    {
        _collisionHandler.CollisionDetected += ProcessCollision;
    }

    protected virtual void OnDisable()
    {
        _collisionHandler.CollisionDetected -= ProcessCollision;
    }

    public virtual void Reset()
    {
        transform.localPosition = _startPosition;
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
        Destroing?.Invoke(this);
        Destroy(gameObject);
    }
}