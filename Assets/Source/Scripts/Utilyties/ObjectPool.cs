using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private T _prefab;

    protected Queue<T> Pool;

    public IEnumerable<T> PooledObjects => Pool;

    private void Awake()
    {
        Pool = new Queue<T>();
    }

    protected virtual T GetObject()
    {
        if (Pool.Count == 0)
        {
            var obj = Instantiate(_prefab, _container, true);

            return obj;
        }

        return Pool.Dequeue();
    }

    protected virtual void PutObject(T obj)
    {
        Pool.Enqueue(obj);
        obj.gameObject.SetActive(false);
    }

    protected virtual void Reset()
    {
        Pool.Clear();
    }
}