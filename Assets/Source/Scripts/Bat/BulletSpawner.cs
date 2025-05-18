using UnityEngine;

public class BulletSpawner : ObjectPool<Projectile>
{
    [SerializeField] private Transform _firePoint;

    public void SpawnBullet()
    {
        Projectile bullet = GetObject();

        bullet.PrefferToDestroyed += PutObject;

        bullet.transform.position = new Vector2(_firePoint.transform.position.x, _firePoint.transform.position.y);
        bullet.transform.rotation = _firePoint.transform.rotation;
        bullet.SetVelocity();
        bullet.gameObject.SetActive(true);

        bullet.StartLifeTimeDecreasing();
    }

    protected override Projectile GetObject()
    {
        return base.GetObject();
    }

    protected override void PutObject(Projectile obj)
    {
        obj.PrefferToDestroyed -= PutObject;

        base.PutObject(obj);
    }

    protected override void Reset()
    {
        base.Reset();
    }
}
