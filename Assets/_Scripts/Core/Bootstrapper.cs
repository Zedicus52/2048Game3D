using Zenject;

namespace Game.Core
{
    public sealed class Bootstrapper : IInitializable
    {
        private readonly ZenjectSceneLoader _zenjectSceneLoader;

        public Bootstrapper(ZenjectSceneLoader zenjectSceneLoader)
        {
            _zenjectSceneLoader = zenjectSceneLoader;
        }

        public void Initialize()
        {
            _zenjectSceneLoader.LoadSceneAsync(1, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}


