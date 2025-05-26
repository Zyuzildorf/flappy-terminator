using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MoverRighToLeft),typeof(Shooter))]
public class EnemyType3 : Enemy, IShootable, IMovable
{
    private MoverRighToLeft _mover;
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

        _mover = GetComponent<MoverRighToLeft>();
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

    public void Move()
    {
        _mover.MoveRightToLeft();
    }

    public void Shoot()
    {
        _shooter.StartShooting();
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