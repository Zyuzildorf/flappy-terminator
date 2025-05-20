using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinWaveMover : MonoBehaviour
{
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    [SerializeField] private bool _inverted;

    private float _sinCenterY;

    private void Start()
    {
        _sinCenterY = transform.position.y;
    }

    public void MoveSinWave()
    {
        Vector2 pos = transform.position;

        float sin = Mathf.Sin(pos.x * _frequency) * _amplitude;

        if(_inverted)
        {
            sin *= -1;
        }

        pos.y = _sinCenterY + sin;

        transform.position = pos;
    }
}
