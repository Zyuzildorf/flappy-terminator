using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action OnHitted;

    public void GetHit()
    {
        OnHitted?.Invoke();
    }
}
