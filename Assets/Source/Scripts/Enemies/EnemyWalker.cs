using Source.Scripts.Interfaces;
using Source.Scripts.Triggers;
using Source.Scripts.Utilities;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    [RequireComponent(typeof(MoverRighToLeft),typeof(Shooter))]
    public class EnemyWalker : Enemy, IShootable, IMovable
    {
        private MoverRighToLeft _mover;
        private Shooter _shooter;
        private bool _canShoot;
    
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
            _shooter.TryShoot();
        }

        public void Reset()
        {
            _shooter.Reset();
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
}