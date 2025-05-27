using System;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameLogic _gameLogic;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private ScoreView _scoreView;
   
    private void OnEnable()
    {
        _gameLogic.PrefferToStart += OnGameStart;
        _gameLogic.PrefferToRestart += OnGameRestart;
        _gameLogic.GameOver += ShowGameOverScreen;
    }

    private void OnDisable()
    {
        _gameLogic.PrefferToStart -= OnGameStart;
        _gameLogic.PrefferToRestart -= OnGameRestart;
        _gameLogic.GameOver -= ShowGameOverScreen;
    }

    private void Awake()
    {
        _endGameScreen.Close();
        _scoreView.HideCurrentScore();
        _scoreView.HideBestScore();
        _scoreView.Open();
        _startScreen.Open();
    }

    private void OnGameRestart()
    {
        _endGameScreen.Close();
        ShowCurrentScore();
    }
    
    private void OnGameStart()
    {
        _startScreen.Close();
        ShowCurrentScore();
    }

    private void ShowGameOverScreen()
    {
        _scoreView.HideCurrentScore();
        _scoreView.Reset();

        _endGameScreen.Open();
        _scoreView.ShowBestScore();
    }

    private void ShowCurrentScore()
    {
        _scoreView.HideBestScore();
        _scoreView.ShowCurrentScore();
    }
}