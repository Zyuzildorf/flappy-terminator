using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Shooter))]
public class EnemyType1 : Enemy
{
    private Mover _mover;
    private Shooter _shooter;

    protected override void Awake()
    {
        base.Awake();

        _mover = GetComponent<Mover>();
        _shooter = GetComponent<Shooter>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
        _mover.MoveTowardsWaypoint();
    }

    protected override void Shoot()
    {
        _shooter.StartShooting();
    }

    protected override void ProccesProjectileCollision(Projectile projectile)
    {
        base.ProccesProjectileCollision(projectile);
    }
}
