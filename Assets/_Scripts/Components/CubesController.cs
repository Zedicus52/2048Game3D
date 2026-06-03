using Game.Core;
using Game.Settings;
using System;
using System.Linq;
using Zenject;
namespace Game.Gameplay
{
    public sealed class CubesController : IInitializable, IDisposable
    {
        public event Action<int> CubeCollides;

        private readonly CubeProvider _cubesProvider;
        private readonly CubeMovement _cubeMovement;

        private readonly CubeEntryStore _cubeEntryStore;
        private readonly CubeSpawnPoint _cubeSpawnPoint;

        public CubesController(CubeMovement cubeMovement, CubeProvider cubeProvider, CubeEntryStore cubeEntryStore, CubeSpawnPoint spawnPoint)
        {
            _cubeSpawnPoint = spawnPoint;
            _cubeEntryStore = cubeEntryStore;
            _cubeMovement = cubeMovement;
            _cubesProvider = cubeProvider;
        }

        public void Initialize()
        {
            _cubeMovement.CubeMovementEnded += SpawnNewCube;
            SpawnNewCube();
        }

        private void SpawnNewCube()
        {
            var cube = _cubesProvider.GetCube();
            cube.SetPosition(_cubeSpawnPoint.SpawnPoint.position);
            var cubeSettings = _cubeEntryStore.CubeSettigs.Random();
            cube.SetCubeSettings(cubeSettings);
            cube.gameObject.SetActive(true);
            cube.CubesCollides += OnCubeCollides;
            _cubeMovement.SetCurrentCube(cube);
        }

        private void OnCubeCollides(Cube initiator, Cube target)
        {
            if (initiator.CubeValue != target.CubeValue)
                return;

            initiator.CubesCollides -= OnCubeCollides;
            var newNumber = initiator.CubeValue * 2;

            var cubeSettings = FindCubeSettings(newNumber);

            target.SetCubeSettings(cubeSettings);
            target.AddImpulse();

            _cubesProvider.ReleaseCube(initiator);
        }

        private CubeEntrySettings FindCubeSettings(int newNumber)
        {
            var cubeSettings = _cubeEntryStore.CubeSettigs.FirstOrDefault(x => x.CubeValue == newNumber);
            if (cubeSettings == null && newNumber == GameSettings.kMaxCubeValue)
                cubeSettings = _cubeEntryStore.MaxCubeEntrySettings;
            return cubeSettings;
        }

        public void Dispose()
        {
            _cubeMovement.CubeMovementEnded -= SpawnNewCube;
            foreach (var item in _cubesProvider.GetAllActiveCubes())
            {
                item.CubesCollides -= OnCubeCollides;
            }
        }
    }
}

