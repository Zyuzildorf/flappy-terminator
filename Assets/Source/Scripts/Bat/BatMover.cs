using System;
using UnityEngine;

namespace Source.Scripts.Bat
{
    public class BatMover : MonoBehaviour
    {
        [SerializeField] private float _flyForce;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _maxRotationZ;
        [SerializeField] private float _minRotationZ;

        private Vector3 _startPosition;
        private Rigidbody2D _rigidbody2D;
        private Quaternion _maxRotation;
        private Quaternion _minRotation;

        public event Action Swinging;

        private void Awake()
        {
            _startPosition = transform.position;
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
            _minRotation = Quaternion.Euler(0, 0, _minRotationZ);

            Reset();
        }

        public void Move()
        {
            _rigidbody2D.velocity = new Vector2(0, _flyForce);
            transform.rotation = _maxRotation;

            Swinging?.Invoke();
        }

        public void Fall()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
        }

        public void Reset()
        {
            transform.position = _startPosition;
            transform.rotation = Quaternion.identity;
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
}