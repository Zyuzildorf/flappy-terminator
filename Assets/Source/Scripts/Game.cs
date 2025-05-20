using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Bat _bat;
    [SerializeField] private Camera _camera;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private ScoreView _scoreView;

    private void OnEnable()
    {
        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
        _bat.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClicked -= OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
        _bat.GameOver -= OnGameOver;
    }

    private void Awake()
    {
        _endGameScreen.Close();
        _scoreView.HideCurrentScore();
        _scoreView.HideBestScore();
        _scoreView.Open();
        _startScreen.Open();
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        _scoreView.HideCurrentScore();
        _scoreView.Reset();

        _endGameScreen.Open();
        _scoreView.ShowBestScore();
    }

    private void OnRestartButtonClick()
    {
        _endGameScreen.Close();
        StartGame();
    }
    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _bat.Reset();
        _scoreView.HideBestScore();
        _scoreView.ShowCurrentScore();
    }
}
