using Game.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class CubeProvider : MonoBehaviour
    {
        [SerializeField] private Cube _cubePrefab;
        [SerializeField] private int _basicCount = 20;

        private ObjectPool<Cube> _cubesPool;

        private void Awake()
        {
            _cubesPool = new ObjectPool<Cube>(_cubePrefab, transform, _basicCount);
        }

        public Cube GetCube()
        {
            return _cubesPool.GetObject();
        }

        public void ReleaseCube(Cube cube)
        {
            cube.ResetCube();
            _cubesPool.ReleaseObject(cube);
        }

        public IEnumerable<Cube> GetAllActiveCubes()
        {
            return _cubesPool.ActiveObjects;
        }

        private void OnDestroy()
        {
            _cubesPool.Dispose();
        }
    }
}

