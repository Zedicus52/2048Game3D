using Game.Gameplay;
using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Core
{
    public sealed class GameplayBootstrapper : MonoInstaller
    {
        [SerializeField] private CubeAccelerationSettings _accelerationSettings;
        [SerializeField] private CubeEntryStore _cubeEntryStore;
        [SerializeField] private CubeSpawnPoint _cubeSpawnPoint;
        [SerializeField] private CubeProvider _cubeProvider;

        public override void InstallBindings()
        {
            Container.BindInstance(_accelerationSettings).AsSingle();
            Container.BindInstance(_cubeEntryStore).AsSingle();
            Container.BindInstance(_cubeSpawnPoint).AsSingle();
            Container.BindInstance(_cubeProvider).AsSingle();

            Container.BindInterfacesAndSelfTo<CubeMovement>().AsSingle();
            Container.BindInterfacesAndSelfTo<CubesController>().AsSingle();
        }
    }

}

