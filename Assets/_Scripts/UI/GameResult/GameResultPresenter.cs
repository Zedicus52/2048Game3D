using Game.Managers;
using System;
using UnityEngine;
using Zenject;
namespace Game.UI
{
    public sealed class GameResultPresenter : IInitializable, IDisposable
    {
        private const string kWinLable = "You win!";
        private const string kLoseLable = "You lose!";
        private const int kStartScene = 1;

        private readonly GameResultView _resultView;
        private readonly ScoreHudView _scoreHudView;
        private readonly IScoreManager _scoreManager;
        private readonly IGameStateService _stateService;
        private readonly ZenjectSceneLoader _sceneLoader;

        public GameResultPresenter(GameResultView view, IScoreManager scoreManager, IGameStateService stateService, 
            ZenjectSceneLoader sceneLoader, ScoreHudView scoreHud)
        {
            _resultView = view;
            _scoreManager = scoreManager;
            _stateService = stateService;
            _sceneLoader = sceneLoader;
            _scoreHudView = scoreHud;
        }

        public void Initialize()
        {
            _resultView.ExitClicked += OnExitClicked;
            _resultView.RestartClicked += OnRestartClicked;
            _stateService.GameEnded += OnGameEnded;
            _resultView.SetVisibility(false);
        }

        public void Dispose()
        {
            _resultView.ExitClicked -= OnExitClicked;
            _resultView.RestartClicked -= OnRestartClicked;
            _stateService.GameEnded -= OnGameEnded;
        }

        private void OnGameEnded(bool isWin)
        {
            _resultView.SetVisibility(true);
            _scoreHudView.SetVisibility(false);
            var label = isWin ? kWinLable : kLoseLable;
            _resultView.SetLabel(label);
            _resultView.SetMaxScore(_scoreManager.GetMaxScore().ToString());
            _resultView.SetCurrentScore(_scoreManager.GetCurrentScore().ToString());
        }

        private void OnExitClicked()
        {
            Application.Quit();
        }

        private void OnRestartClicked()
        {
            _sceneLoader.LoadScene(kStartScene);
        }
    }
}