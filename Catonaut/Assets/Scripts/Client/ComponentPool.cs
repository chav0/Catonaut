using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Client
{
    public class ComponentPool<T> : IDisposable 
        where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly bool _enableOnGet;
        private readonly Transform _parent;
        private readonly Stack<T> _inactiveObjects;

        public ComponentPool(T prefab, int initialSize = 10, bool activateOnGet = true, Transform parent = null)
        {
            _prefab = prefab;
            _inactiveObjects = new Stack<T>(initialSize);
            _enableOnGet = activateOnGet;
            _parent = parent;
        }

        public T Get()
        {
            T res = null;
            while (_inactiveObjects.Count > 0)
            {
                res = _inactiveObjects.Pop();
                if (res != null)
                    break;
                Debug.LogWarning($"Pooled component '{typeof(T).FullName}' was destroyed in outer scope.");
            }
            if (res == null)
            {
                res = Object.Instantiate<T>(_prefab, _parent, worldPositionStays:false);
            }
            if (_enableOnGet)
            {
                res.gameObject.SetActive(true);
            }
            return res;
        }

        public void Return(T item)
        {
            if (item == null)
            {
                Debug.LogWarning($"Can't return destroyed component '{typeof(T).FullName}' to the pool");
                return;
            }
            item.gameObject.SetActive(false);
            _inactiveObjects.Push(item);
        }

        public void Dispose()
        {
            while (_inactiveObjects.Count > 0)
            {
                Object.Destroy(_inactiveObjects.Pop());
            }
        }
    }
}
