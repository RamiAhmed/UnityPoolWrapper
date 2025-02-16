using System;
using UnityEngine;

namespace PoolWrapper
{
    public sealed class MonoBehaviourPool : SimpleBehaviourPool<MonoBehaviour>
    {
        public MonoBehaviourPool(
            Transform host,
            MonoBehaviour prefab, 
            Action<MonoBehaviour> actionOnGet = null, 
            Action<MonoBehaviour> actionOnRelease = null, 
            Action<MonoBehaviour> actionOnDestroy = null,
            bool collectionCheck = true, 
            int defaultCapacity = 10, 
            int maxSize = 10000) 
            : base(host, prefab, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, defaultCapacity, maxSize)
        {
        }
    }
}