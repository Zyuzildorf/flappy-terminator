using UnityEngine;

[RequireComponent(typeof(MoverTowardsWaypoint), typeof(Shooter))]
public class EnemyType1 : Enemy, IShootable, IMovable
{
    private MoverTowardsWaypoint _mover;
    private Shooter _shooter;
    private bool _canShoot;

    public bool CanShoot
    {
        get => _canShoot;
        set => _canShoot = value;
    }

    protected override void Awake()
    {
        base.Awake();

        _mover = GetComponent<MoverTowardsWaypoint>();
        _shooter = GetComponent<Shooter>();
        _canShoot = false;
    }

    private void Update()
    {
        Move();

        if (_canShoot)
        {
            Shoot();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _canShoot = false;
    }

    public void Shoot()
    {
        _shooter.StartShooting();
    }

    public void Move()
    {
        _mover.MoveTowardsWaypoint();
    }

    protected override void ProcessCollision(IInteractable interactable)
    {
        base.ProcessCollision(interactable);

        if (interactable is ShootActivationTrigger)
        {
            _canShoot = true;
        }
    }
}