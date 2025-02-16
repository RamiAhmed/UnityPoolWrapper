using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PoolWrapper
{
    public class SimpleBehaviourPool<T> : PoolBase<T> where T : Component
    {
        private readonly Transform _host;
        private readonly T _prefab;

        public SimpleBehaviourPool(
            Transform host,
            T prefab,
            Action<T> actionOnGet = null,
            Action<T> actionOnRelease = null,
            Action<T> actionOnDestroy = null,
            bool collectionCheck = true,
            int defaultCapacity = 10,
            int maxSize = 10000)
            : base(
                null,
                actionOnGet,
                actionOnRelease,
                actionOnDestroy,
                collectionCheck,
                defaultCapacity,
                maxSize)
        {
            _host = host;
            _prefab = prefab;
        }

        public T Get(string name)
        {
            var item = Get();

            if (!string.IsNullOrEmpty(name))
                item.name = name;

            return item;
        }

        public T Get(Vector3 position, string name = null)
        {
            var item = Get(name);
            item.transform.position = position;
            return item;
        }

        public T Get(Vector3 position, Quaternion rotation, string name = null)
        {
            var item = Get(name);
            item.transform.SetPositionAndRotation(position, rotation);
            return item;
        }

        protected override T Create()
        {
            var instance = Object.Instantiate(_prefab, _host);
            instance.gameObject.SetActive(false);
            return instance;
        }

        protected override void OnGet(T item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnRelease(T item)
        {
            item.gameObject.SetActive(false);
        }
    }
}