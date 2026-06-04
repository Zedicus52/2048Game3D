using System;
namespace Game.Managers
{
    public sealed class GameStateController : IGameStateService
    {
        public event Action<bool> GameEnded;

        public GameState CurrentState { get; private set; }

        public GameStateController()
        {
            CurrentState = GameState.Playing;
        }

        public void LoseGame()
        {
            if (CurrentState != GameState.Playing) 
                return;

            CurrentState = GameState.Lost;
            GameEnded?.Invoke(false);
        }

        public void WinGame()
        {
            if (CurrentState != GameState.Playing)
                return;

            CurrentState = GameState.Won;
            GameEnded?.Invoke(true);
        }
    }
}

