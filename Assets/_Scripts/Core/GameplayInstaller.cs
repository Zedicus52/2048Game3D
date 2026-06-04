using Game.Gameplay;
using Game.Managers;
using Game.Settings;
using Game.UI;
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
        [Header("UI")]
        [SerializeField] private ScoreHudView _scoreHudView;
        [SerializeField] private GameResultView _gameResultView;

        public override void InstallBindings()
        {
            BindAccelerationSettings();
            BindCubeEntryStore();
            BindCubeSpawnPoint();
            BindCubeProvider();

            BindCubeMovement();
            BindCubesController();
            BindScoreManager();
            BindGameStateController();

            BindScoreHud();
            BindGameResultHud();
        }

        private void BindGameResultHud()
        {
            Container
                .Bind<GameResultView>()
                .FromInstance(_gameResultView)
                .AsSingle();

            Container
                .BindInterfacesTo<GameResultPresenter>()
                .AsSingle();
        }

        private void BindScoreHud()
        {
            Container
                .Bind<ScoreHudView>()
                .FromInstance(_scoreHudView)
                .AsSingle();

            Container
                .BindInterfacesTo<ScoreHudPresenter>()
                .AsSingle();
        }

        private void BindGameStateController()
        {
            Container
                .BindInterfacesAndSelfTo<GameStateController>()
                .AsSingle();
        }

        private void BindScoreManager()
        {
            Container
                .BindInterfacesTo<ScoreManager>()
                .AsSingle();
        }

        private void BindCubesController()
        {
            Container.BindInterfacesAndSelfTo<CubesController>().AsSingle();
        }

        private void BindCubeMovement()
        {
            Container.BindInterfacesAndSelfTo<CubeMovement>().AsSingle();
        }

        private void BindCubeProvider()
        {
            Container.BindInstance(_cubeProvider).AsSingle();
        }

        private void BindCubeSpawnPoint()
        {
            Container.BindInstance(_cubeSpawnPoint).AsSingle();
        }

        private void BindCubeEntryStore()
        {
            Container.BindInstance(_cubeEntryStore).AsSingle();
        }

        private void BindAccelerationSettings()
        {
            Container.BindInstance(_accelerationSettings).AsSingle();
        }
    }

}

