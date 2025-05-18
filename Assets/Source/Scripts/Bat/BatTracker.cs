using UnityEngine;

public class BatTracker : MonoBehaviour
{
    [SerializeField] private Bat _bat;
    [SerializeField] private float _xOffset;

    private void Update()
    {
        var position = transform.position;
        position.x = _bat.transform.position.x + _xOffset;
        transform.position = position;
    }
}
