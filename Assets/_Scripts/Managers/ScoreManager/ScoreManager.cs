using System;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class ScoreData
    {
        public int Score;
        public int MaxScore;
    }

    public sealed class ScoreManager : IScoreManager, IInitializable, IDisposable
    {
        public event Action<int> ScoreChanged;

        public ScoreData ScoreData { get; private set; }


        private readonly ISaveManager _saveManager;

        public ScoreManager(ISaveManager saveManager)
        {
            _saveManager = saveManager;
        }
        public void Initialize()
        {
            ScoreData = _saveManager.Load<ScoreData>(DataType.MaxScore);
            ScoreData.Score = 0;
            OnScoreChanged(ScoreData.Score);
        }

        public void Dispose()
        {
            TryToUpdateMaxScore();
        }

        public void AddScore(int score)
        {
            if (score <= 0)
                return;

            ScoreData.Score += score;

            TryToUpdateMaxScore();

            OnScoreChanged(ScoreData.Score);
        }

        public int GetCurrentScore()
        {
            return ScoreData.Score;
        }

        public int GetMaxScore()
        {
            return ScoreData.MaxScore;
        }

        private void TryToUpdateMaxScore()
        {
            if (NeedToUpdateMaxScore())
            {
                UpdateMaxScore();
            }
        }

        private void UpdateMaxScore()
        {
            ScoreData.MaxScore = ScoreData.Score;
            _saveManager.Save(DataType.MaxScore, ScoreData);
        }

        private bool NeedToUpdateMaxScore()
        {
            return ScoreData.Score > ScoreData.MaxScore;
        }

        private void OnScoreChanged(int score)
        {
            ScoreChanged?.Invoke(score);
        }


    }
}