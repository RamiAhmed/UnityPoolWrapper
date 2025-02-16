using System;

namespace PoolWrapper
{
    public class SimplePool<T> : PoolBase<T> where T : class, new()
    {
        public SimplePool(
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
        }

        protected override T Create()
        {
            var instance = new T();
            OnCreate(instance);
            return instance;
        }

        protected virtual void OnCreate(T item)
        {
            /* NOOP */
        }
    }
}