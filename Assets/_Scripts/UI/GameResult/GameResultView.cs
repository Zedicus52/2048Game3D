using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Game.UI
{
    public sealed class GameResultView : MonoBehaviour
    {
        public event Action RestartClicked;
        public event Action ExitClicked;

        [SerializeField] private GameObject _mainContent;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TMP_Text _labelText;
        [SerializeField] private TMP_Text _currentScore;
        [SerializeField] private TMP_Text _maxScore;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartClicked);
            _exitButton.onClick.AddListener(OnExitClicked);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        public void SetLabel(string label)
        {
            _labelText.text = label;
        }

        public void SetCurrentScore(string score)
        {
            _currentScore.text = score;
        }

        public void SetMaxScore(string maxScore)
        {
            _maxScore.text = maxScore;
        }

        public void SetVisibility(bool isVisible)
        {
            _mainContent.SetActive(isVisible);
        }

        private void OnRestartClicked()
        {
            RestartClicked?.Invoke();
        }
        
        private void OnExitClicked()
        {
            ExitClicked?.Invoke();
        }
    }
}