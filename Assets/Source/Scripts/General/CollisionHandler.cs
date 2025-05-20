using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> CollisionDetected;
    public event Action<Projectile> ProjectileDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable))
        {
            CollisionDetected?.Invoke(interactable);
        }

        if(collision.TryGetComponent(out Projectile projectile))
        {
            ProjectileDetected?.Invoke(projectile);
        }
    }
}
