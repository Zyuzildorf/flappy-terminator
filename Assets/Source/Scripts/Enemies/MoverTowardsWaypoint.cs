using System;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class MoverTowardsWaypoint : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _wayPoint;

        private bool _isOnPosition;

        public event Action ReachedPosition;
    
        private void Awake()
        {
            _isOnPosition = false;
        }

        public void MoveTowardsWaypoint()
        {
            Vector2 position = transform.position;

            position = Vector2.MoveTowards(position, _wayPoint.position,
                _speed * Time.deltaTime);

            transform.position = position;

            if (CheckIsOnPosition())
            {
                ReachedPosition?.Invoke();
            }
        }

        private bool CheckIsOnPosition()
        {
            if (Mathf.Abs(transform.position.x - _wayPoint.position.x) < 0.01)
            {
                return _isOnPosition = true;
            }

            return _isOnPosition;
        }
    }
}