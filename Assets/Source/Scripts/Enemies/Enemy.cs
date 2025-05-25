using System;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public abstract class Enemy : MonoBehaviour, IInteractable, IScorable
{
    [SerializeField] private int _addScore;

    private CollisionHandler _collisionHandler;

    private Vector2 _startPosition;
    private bool _isActive = false;

    public event Action<int> OnScoreValueAdd;
    public event Action<Enemy> PrefferedToDestroy;

    protected virtual void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        _startPosition = transform.position;
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
        transform.position = _startPosition;
    }
    
    protected virtual void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Projectile projectile && projectile.IsEnemy == false && _isActive)
        {
            projectile.CallEvent();

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
        PrefferedToDestroy?.Invoke(this);
        OnScoreValueAdd?.Invoke(_addScore);
    }
}