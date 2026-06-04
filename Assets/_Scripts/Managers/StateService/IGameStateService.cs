using System;
namespace Game.Managers
{
    public interface IGameStateService
    {
        GameState CurrentState { get; }

        event Action<bool> GameEnded;

        void WinGame();
        void LoseGame();
    }
}

