using System;
using UnityEngine.Pool;

namespace PoolWrapper
{
    public abstract class PoolBase<T> : IDisposable where T : class
    {
        private readonly ObjectPool<T> _pool;

        public PoolBase(
            Func<T> createFunc = null,
            Action<T> actionOnGet = null,
            Action<T> actionOnRelease = null,
            Action<T> actionOnDestroy = null,
            bool collectionCheck = true,
            int defaultCapacity = 10,
            int maxSize = 10000)
        {
            _pool = new ObjectPool<T>(
                createFunc ?? Create,
                actionOnGet,
                actionOnRelease,
                actionOnDestroy,
                collectionCheck,
                defaultCapacity,
                maxSize);
        }

        public void Dispose()
        {
            OnDispose();
            _pool.Dispose();
        }

        public T Get()
        {
            var item = _pool.Get();
            OnGet(item);
            return item;
        }

        public void Release(T obj)
        {
            OnRelease(obj);
            _pool.Release(obj);
        }

        public void Clear()
        {
            OnClear();
            _pool.Clear();
        }

        protected abstract T Create();

        protected virtual void OnGet(T item)
        {
            /* NOOP */
        }

        protected virtual void OnRelease(T item)
        {
            /* NOOP */
        }

        protected virtual void OnClear()
        {
            /* NOOP */
        }

        protected virtual void OnDispose()
        {
            /* NOOP */
        }
    }
}