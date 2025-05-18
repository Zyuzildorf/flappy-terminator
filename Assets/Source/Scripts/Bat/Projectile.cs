using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime = 5;
    [SerializeField] private Rigidbody2D _rigidbody;

    public event Action<Projectile> PrefferToDestroyed;

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void SetVelocity()
    {
        _rigidbody.velocity = new Vector2(_speed, 0);
    }

    public void StartLifeTimeDecreasing()
    {
        StartCoroutine(DecreaseLifeTime());
    }

    private IEnumerator DecreaseLifeTime()
    {
        while (_lifeTime >= 0)
        {
            _lifeTime -= Time.deltaTime;
            yield return null;
        }

        PrefferToDestroyed?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopCoroutine(DecreaseLifeTime());

        if(collision.TryGetComponent(out Enemy enemy))
        {
            enemy.GetHit();
        }

        PrefferToDestroyed?.Invoke(this);
    }
}
