using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime = 2;
    [SerializeField] private bool _isEnemy = false;

    private float _currentLifeTime;

    public event Action<Projectile> PrefferToDestroyed;
    public bool IsEnemy => _isEnemy;

    private void Awake()
    {
        _currentLifeTime = _lifeTime;
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnDisable()
    {
        PrefferToDestroyed?.Invoke(this);
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
        while (_currentLifeTime >= 0)
        {
            _currentLifeTime -= Time.deltaTime;
            yield return null;
        }

        Die();
    }

    private void Die()
    {
        PrefferToDestroyed?.Invoke(this);
        _currentLifeTime = _lifeTime;
        _rigidbody.velocity = Vector2.zero;
    }
}