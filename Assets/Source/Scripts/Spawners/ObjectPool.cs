using Source.Scripts.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Source.Scripts.Spawners
{
    public class ObjectsPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _poolCapacity = 10;
        [SerializeField] private int _poolMaxSize = 15;

        private Transform _container;
        private ObjectPool<T> _pool;
        private List<T> _allCreatedObjects = new List<T>();

        private void Awake()
        {
            _container = BulletContainer.Container;

            _pool = new ObjectPool<T>(
                createFunc: () => CreateObject(),
                actionOnGet: (obj) => OnGet(obj),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj.gameObject),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize);
        }

        public void Reset()
        {
            foreach (T obj in _allCreatedObjects)
            {
                if (obj.isActiveAndEnabled)
                {
                    _pool.Release(obj);
                }
            }
        }

        protected virtual T CreateObject()
        {
            T obj = Instantiate(_prefab, _container, true);
            _allCreatedObjects.Add(obj);
            return obj;
        }

        protected virtual void GetObject()
        {
            _pool.Get();
        }

        protected virtual void ReleaseObject(T obj)
        {
            _pool.Release(obj);
        }

        protected virtual void OnGet(T obj)
        {
            obj.gameObject.SetActive(true);
        }
    }
}