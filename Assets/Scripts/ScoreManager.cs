using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
   [SerializeField] private TMP_Text _scoreText;
   [SerializeField] private TMP_Text _bestScoreText;
   [SerializeField] private TMP_Text _finishText;
   [SerializeField] private TMP_Text _label;
   private int _score;
   private int _bestScore;
   private SaveLoadSystem _saveLoadSystem;

   private void Awake()
   {
      _saveLoadSystem = new SaveLoadSystem();
      _bestScore = _saveLoadSystem.ReadScore();
   }

   public void AddScore(int score)
   {
      _score += score;
      _scoreText.text = _score.ToString();
      if (_score > _bestScore)
      {
         _bestScore = _score;
         _saveLoadSystem.SaveScore(_bestScore);
      }
   }

   public void FinisGame(bool isWin)
   {
      _scoreText.enabled = false;
      _label.text = isWin ? "You win!" : "You lose!";
      _bestScoreText.text = "Best score:\n" + _bestScore;
      _finishText.text = "Score:\n" + _score;
   }
}
