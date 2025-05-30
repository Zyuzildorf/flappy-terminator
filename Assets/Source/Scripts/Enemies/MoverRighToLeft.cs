using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class MoverRighToLeft : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public void MoveRightToLeft()
        {
            Vector2 position = transform.position;

            position.x -= _speed * Time.deltaTime;

            transform.position = position;
        }
    }
}