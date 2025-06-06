using UnityEngine;

namespace Source.Scripts.Spawners
{
    public class ProjectileSpawner : ObjectsPool<Projectile>
    {
        [SerializeField] private Transform _firePoint;

        public void SpawnBullet()
        {
            GetObject();
        }

        protected override void ActionOnGet(Projectile obj)
        {
            obj.PrefferToDestroyed += ReleaseObject;
            obj.transform.position = _firePoint.position;

            base.ActionOnGet(obj);

            obj.SetVelocity(GetDirection());
            obj.StartLifeTimeDecreasing();
        }

        private Vector2 GetDirection()
        {
            Vector2 leftRightDirection;
            float distance = transform.position.x - _firePoint.position.x;

            if (Mathf.Sign(distance) > 0)
            {
                leftRightDirection = Vector2.left;
            }
            else
            {
                leftRightDirection = Vector2.right;
            }

            return (transform.rotation * leftRightDirection).normalized;
        }

        protected override void ReleaseObject(Projectile obj)
        {
            obj.PrefferToDestroyed -= ReleaseObject;

            base.ReleaseObject(obj);
        }
    }
}