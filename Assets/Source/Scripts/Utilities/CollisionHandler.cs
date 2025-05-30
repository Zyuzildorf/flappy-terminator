using System;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Utilities
{
    public class CollisionHandler : MonoBehaviour
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
}