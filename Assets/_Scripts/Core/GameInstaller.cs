using Game.Managers;
using UnityEngine;
using Zenject;

namespace Game.Core
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private PCInputManager _inputManagerPrefab;

        public override void InstallBindings()
        {
            BindInputManager();
            BindSaveManager();

            BindBootstrapper();
        }

        private void BindBootstrapper()
        {
            Container.BindInterfacesTo<Bootstrapper>().AsSingle();
        }

        private void BindSaveManager()
        {
            Container.Bind<ISaveManager>()
               .To<PlayerPrefsSaveManager>()
               .AsSingle()
               .NonLazy();
        }

        private void BindInputManager()
        {
            Container.Bind<IInputManager>()
                .To<PCInputManager>()
                .FromComponentInNewPrefab(_inputManagerPrefab)
                .AsSingle()
                .NonLazy();
        }
    }
}