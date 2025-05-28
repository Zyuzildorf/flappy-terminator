using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MoverTowardsWaypoint), typeof(MoverRighToLeft), typeof(Shooter))]
public class EnemyType1 : Enemy, IShootable, IMovable
{
    [SerializeField] private float _delayBeforeMove;

    private MoverTowardsWaypoint _moverTowardsWaypoint;
    private MoverRighToLeft _moverRighToLeft;
    private Shooter _shooter;
    private bool _canShoot;
    private bool _isDelayOver;

    public bool CanShoot
    {
        get => _canShoot;
        set => _canShoot = value;
    }

    protected override void Awake()
    {
        base.Awake();

        _moverTowardsWaypoint = GetComponent<MoverTowardsWaypoint>();
        _moverRighToLeft = GetComponent<MoverRighToLeft>();
        _shooter = GetComponent<Shooter>();
        _canShoot = false;
        _isDelayOver = false;
    }

    private void Update()
    {
        Move();

        if (_canShoot)
        {
            Shoot();
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _moverTowardsWaypoint.ReachedPosition += StartDelay;
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
        if (_isDelayOver == false)
        {
            _moverTowardsWaypoint.MoveTowardsWaypoint();
        }
        else
        {
            _moverRighToLeft.MoveRightToLeft();
        }
    }

    protected override void ProcessCollision(IInteractable interactable)
    {
        base.ProcessCollision(interactable);

        if (interactable is ShootActivationTrigger)
        {
            _canShoot = true;
        }
    }

    private void StartDelay()
    {
        StartCoroutine(DelayBeforeMove());
    }
    
    private IEnumerator DelayBeforeMove()
    {
        yield return new WaitForSeconds(_delayBeforeMove);
        _isDelayOver = true;
    }
}