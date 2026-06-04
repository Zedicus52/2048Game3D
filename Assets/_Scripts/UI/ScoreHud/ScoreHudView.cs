using TMPro;
using UnityEngine;
namespace Game.UI
{
    public sealed class ScoreHudView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private GameObject _hudPanel;

        public void UpdateScore(string score)
        {
            _scoreText.text = score;
        }

        public void SetVisibility(bool isVisible)
        {
            _hudPanel.SetActive(isVisible);
        }
    }
}