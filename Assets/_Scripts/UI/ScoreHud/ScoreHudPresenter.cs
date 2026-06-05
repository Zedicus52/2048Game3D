using Game.Managers;
using Game.UI;
using System;
using Zenject;

public sealed class ScoreHudPresenter : IInitializable, IDisposable
{
    private readonly ScoreHudView _view;
    private readonly IScoreManager _scoreManager;
    private readonly IGameStateService _stateService;

    public ScoreHudPresenter(ScoreHudView view, IScoreManager scoreManager, IGameStateService stateService)
    {
        _view = view;
        _scoreManager = scoreManager;
        _stateService = stateService;
    }

    public void Initialize()
    {
        _scoreManager.ScoreChanged += OnScoreChanged;
        _stateService.GameEnded += OnGameEnded;

        _view.UpdateScore(_scoreManager.GetCurrentScore().ToString());
    }

    public void Dispose()
    {
        _scoreManager.ScoreChanged -= OnScoreChanged;
        _stateService.GameEnded -= OnGameEnded;
    }

    private void OnScoreChanged(int score)
    {
        _view.UpdateScore(score.ToString());
    }

    private void OnGameEnded(bool isWin)
    {
        _view.SetVisibility(false);
    }
}
