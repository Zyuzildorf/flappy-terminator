using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletSpawner))]
public class BatAttacker : MonoBehaviour
{
    [SerializeField] private float _attackDelay = 0.1f;

    private BulletSpawner _bulletSpawner;
    private WaitForSeconds _waitForSeconds;
    private bool _isAttackDelayOver;

    private void Awake()
    {
        _bulletSpawner = GetComponent<BulletSpawner>();
        _waitForSeconds = new WaitForSeconds(_attackDelay);
        _isAttackDelayOver = true;
    }

    public void Attack()
    {
        if (_isAttackDelayOver == false)
            return;

        _bulletSpawner.SpawnBullet();
        StartCoroutine(WaitForNextAttack());
    }

    private IEnumerator WaitForNextAttack()
    {
        _isAttackDelayOver = false;
        yield return _waitForSeconds;
        _isAttackDelayOver = true;
    }
}
