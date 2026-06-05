using System;

namespace Game.Managers
{
    public interface IScoreManager
    {
        event Action<int> ScoreChanged;

        void AddScore(int score);
        int GetCurrentScore();
        int GetMaxScore();
    }
}