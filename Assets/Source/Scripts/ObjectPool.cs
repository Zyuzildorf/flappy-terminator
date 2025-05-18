using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private T _prefab;

    protected Queue<T> _pool;

    public IEnumerable<T> PooledObjects => _pool;

    private void Awake()
    {
        _pool = new Queue<T>();
    }

    protected virtual T GetObject()
    {
        if (_pool.Count == 0)
        {
            var obj = Instantiate(_prefab);
            obj.transform.parent = _container;

            return obj;
        }

        return _pool.Dequeue();
    }

    protected virtual void PutObject(T obj)
    {
        _pool.Enqueue(obj);
        obj.gameObject.SetActive(false);
    }

    protected virtual void Reset()
    {
        _pool.Clear();
    }
}
