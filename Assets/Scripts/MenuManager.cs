using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Transform _finishMenu;
    [SerializeField] private Button _exitBtn;
    [SerializeField] private Button _retryBtn;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        
        _exitBtn.onClick.AddListener(Application.Quit);
        _retryBtn.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    private void OnDisable()
    {
        
        _exitBtn.onClick.RemoveListener(Application.Quit);
        _retryBtn.onClick.RemoveListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex)); 
    }

    public void FinisGame(bool win = true)
    {
        Time.timeScale = 0f;
        _finishMenu.gameObject.SetActive(true);
    }
}
