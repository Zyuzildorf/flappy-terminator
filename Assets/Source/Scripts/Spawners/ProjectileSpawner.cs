using UnityEngine;

namespace Source.Scripts.Spawners
{
    public class ProjectileSpawner : ObjectPool<Projectile>
    {
        [SerializeField] private Transform _firePoint;

        public void SpawnBullet()
        {
            Projectile bullet = GetObject();

            bullet.PrefferToDestroyed += PutObject;

            bullet.transform.position = _firePoint.position;
            bullet.gameObject.SetActive(true);
            bullet.SetVelocity(GetDirection());

            bullet.StartLifeTimeDecreasing();
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

        protected override void PutObject(Projectile obj)
        {
            obj.PrefferToDestroyed -= PutObject;

            base.PutObject(obj);
        }
    }
}