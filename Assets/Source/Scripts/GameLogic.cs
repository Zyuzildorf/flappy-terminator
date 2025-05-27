using System;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Bat _bat;
    [SerializeField] private InputReader _inputReader;

    private bool _isStartMenu;
    private bool _isGameOverMenu;

    public event Action GameOver;
    public event Action PrefferToStart;
    public event Action PrefferToRestart;

    private void OnEnable()
    {
        _bat.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _bat.GameOver -= OnGameOver;
    }

    private void Start()
    {
        _isStartMenu = true;
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (_inputReader.IsSpaceBarPressed)
        {
            ProcessInput();
        }
    }

    private void ProcessInput()
    {
        if (_isStartMenu)
        {
            _isStartMenu = false;

            PrefferToStart?.Invoke();
            StartGame();
        }

        if (_isGameOverMenu)
        {
            PrefferToRestart?.Invoke();
            StartGame();
        }
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        _isGameOverMenu = true;
        GameOver?.Invoke();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _isGameOverMenu = false;
        _bat.Reset();
        _spawner.Reset();
    }
}