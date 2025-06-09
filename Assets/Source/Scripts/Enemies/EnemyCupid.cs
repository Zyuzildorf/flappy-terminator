using System.Collections;
using Source.Scripts.Interfaces;
using Source.Scripts.Triggers;
using Source.Scripts.Utilities;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    [RequireComponent(typeof(MoverTowardsWaypoint), typeof(MoverRighToLeft), typeof(Shooter))]
    public class EnemyCupid : Enemy, IShootable, IMovable
    {
        [SerializeField] private float _delayBeforeMove;

        private MoverTowardsWaypoint _moverTowardsWaypoint;
        private MoverRighToLeft _moverRightToLeft;
        private Shooter _shooter;
        private bool _canShoot;
        private bool _isDelayOver;

        
        protected override void Awake()
        {
            base.Awake();

            _moverTowardsWaypoint = GetComponent<MoverTowardsWaypoint>();
            _moverRightToLeft = GetComponent<MoverRighToLeft>();
            _shooter = GetComponent<Shooter>();
            _canShoot = false;
            _isDelayOver = false;
        }

        public void Reset()
        {
            _shooter.Reset();
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
            _shooter.TryShoot();
        }

        public void Move()
        {
            if (_isDelayOver == false)
            {
                _moverTowardsWaypoint.MoveTowardsWaypoint();
            }
            else
            {
                _moverRightToLeft.MoveRightToLeft();
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
}