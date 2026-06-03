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

            Container.BindInterfacesTo<Bootstrapper>().AsSingle();
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