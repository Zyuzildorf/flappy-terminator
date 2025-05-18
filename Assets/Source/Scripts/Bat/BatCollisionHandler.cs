using System;
using UnityEngine;

public class BatCollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> CollisionDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable))
        {
            CollisionDetected?.Invoke(interactable);
        }
    }
}
