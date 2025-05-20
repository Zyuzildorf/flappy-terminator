using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _wayPoint;

    public void MoveRightToLeft()
    {
        Vector2 position = transform.position;

        position.x -= _speed * Time.deltaTime;

        transform.position = position;
    }

    public void MoveTowardsWaypoint()
    {
        Vector2 position = transform.position;

        position = Vector2.MoveTowards(position, _wayPoint.position, _speed * Time.deltaTime);

        transform.position = position;
    }
}
