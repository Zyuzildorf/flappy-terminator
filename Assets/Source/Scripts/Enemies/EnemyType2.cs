using UnityEngine;

[RequireComponent(typeof(Mover), typeof(SinWaveMover))]
public class EnemyType2 : Enemy
{
    private Mover _mover;
    private SinWaveMover _sinMover;

    protected override void Awake()
    {
        base.Awake();
        _mover = GetComponent<Mover>();
        _sinMover = GetComponent<SinWaveMover>();
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
        _mover.MoveRightToLeft();
        _sinMover.MoveSinWave();
    }

    protected override void Shoot()
    {
        
    }

    protected override void ProccesProjectileCollision(Projectile projectile)
    {
        base.ProccesProjectileCollision(projectile);
    }
}
