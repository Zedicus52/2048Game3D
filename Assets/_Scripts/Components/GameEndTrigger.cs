using Game.Managers;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public sealed class GameEndTrigger : MonoBehaviour
    {
        private IGameStateService _stateService;

        [Inject]
        private void Construct(IGameStateService stateService)
        {
            _stateService = stateService;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Cube cube))
            {
                if (cube.IsMoving() == false)
                {
                    _stateService.LoseGame();
                }
            }
        }
    }
}