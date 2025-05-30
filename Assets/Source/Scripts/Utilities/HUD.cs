using Source.Scripts.UI;
using UnityEngine;

namespace Source.Scripts.Utilities
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private GameLogic _gameLogic;
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private EndGameScreen _endGameScreen;
        [SerializeField] private SettingsMenuScreen _settingsMenuScreen;
        [SerializeField] private ScoreView _scoreView;
   
        private void OnEnable()
        {
            _gameLogic.PreferToStart += OnGameStart;
            _gameLogic.PreferToRestart += OnGameRestart;
            _gameLogic.GameOver += ShowGameOverScreen;
            _gameLogic.PreferToCloseSettingsMenu += _settingsMenuScreen.Close;
            _gameLogic.PreferToOpenSettingsMenu += _settingsMenuScreen.Open;

        }

        private void OnDisable()
        {
            _gameLogic.PreferToStart -= OnGameStart;
            _gameLogic.PreferToRestart -= OnGameRestart;
            _gameLogic.GameOver -= ShowGameOverScreen;
        }

        private void Awake()
        {
            _endGameScreen.Close();
            _settingsMenuScreen.Close();
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

            _endGameScreen.Open();
            _scoreView.ShowBestScore();
        }

        private void ShowCurrentScore()
        {
            _scoreView.HideBestScore();
            _scoreView.ShowCurrentScore();
        }
    }
}