using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Utils
{
    public sealed class ObjectPool<T> where T : MonoBehaviour
    {
        public List<T> ActiveObjects => _activeObjects;

        private readonly T _prefab;
        private readonly int _basicCount;
        private readonly Transform _parentObject;
        private readonly List<T> _poolObjects;
        private readonly List<T> _activeObjects;

        public ObjectPool(T prefab, Transform parentObject, int basicCount = 0)
        {
            _prefab = prefab;
            _parentObject = parentObject;
            _basicCount = basicCount;
            _poolObjects = new List<T>();
            _activeObjects = new List<T>();
            InitializePool();
        }

        private void InitializePool()
        {
            for (int i = 0; i < _basicCount; i++)
            {
                _poolObjects.Add(CreateObject());
            }
        }

        private T CreateObject(bool isVisibly = false)
        {
            var obj = Object.Instantiate(_prefab, _parentObject);
            obj.gameObject.SetActive(isVisibly);
            return obj;
        }

        public T GetObject()
        {
            var gameObject = GetFreeObjectFromPool();

            if (gameObject == null)
                return CreateNewObjectInstance();

            return gameObject;
        }

        private T GetFreeObjectFromPool()
        {
            for (int i = 0; i < _poolObjects.Count; i++)
            {
                var obj = _poolObjects[i];
                if (obj.gameObject.activeInHierarchy == false)
                {
                    _activeObjects.Add(obj);
                    _poolObjects.RemoveAt(i);
                    return obj;
                }
            }

            return null;
        }

        private T CreateNewObjectInstance()
        {
            var newObj = CreateObject(true);
            _activeObjects.Add(newObj);
            return newObj;
        }

        public void ReleaseObject(T obj)
        {
            if (_activeObjects.Remove(obj) == false)
                return;

            ResetObject(obj);
        }

        private void ResetObject(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.gameObject.transform.SetParent(_parentObject, false);
            _poolObjects.Add(obj);
        }

        public void ReleaseAllObjects()
        {
            for (int i = _activeObjects.Count - 1; i >= 0; i--)
            {
                ReleaseObject(_activeObjects[i]);
            }

            _activeObjects.Clear();
        }

        public void Dispose()
        {
            ReleaseAllObjects();
            DestroyAllObjects();
            _poolObjects.Clear();
        }

        private void DestroyAllObjects()
        {
            foreach (var obj in _poolObjects)
            {
                Object.Destroy(obj);
            }
        }
    }
}