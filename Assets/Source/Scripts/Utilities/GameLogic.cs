using System;
using Source.Scripts.Spawners;
using UnityEngine;

namespace Source.Scripts.Utilities
{
    public class GameLogic : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Bat.Bat _bat;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private ScoreCounter _scoreCounter;

        private bool _isStartScreenOpen;
        private bool _isGameOverScreenOpen;
        private bool _isSettingsMenuOpen;

        public event Action GameOver;
        public event Action PreferToStart;
        public event Action PreferToRestart;
        public event Action PreferToOpenSettingsMenu;
        public event Action PreferToCloseSettingsMenu;

        private void OnEnable()
        {
            _bat.GameOver += OnGameOver;
            _spawner.WaveSpawned += _scoreCounter.AddScorable;
            _inputReader.SpaceBarPressed += ProcessInput;
            _inputReader.KeyEscapePressed += TryOpenSettingsMenu;
        }

        private void OnDisable()
        {
            _bat.GameOver -= OnGameOver;
            _spawner.WaveSpawned -= _scoreCounter.AddScorable;
            _inputReader.SpaceBarPressed -= ProcessInput;
            _inputReader.KeyEscapePressed -= TryOpenSettingsMenu;
        }

        private void Start()
        {
            _isSettingsMenuOpen = false;
            _isStartScreenOpen = true;
            PauseGame();
        }
        
        private void TryOpenSettingsMenu()
        {
            if (_isGameOverScreenOpen) return;

            if (_isStartScreenOpen)
            {
                if (_isSettingsMenuOpen)
                {
                    CloseSettingsMenu();
                }
                else
                {
                    OpenSettingsMenu();
                }
            }
            else
            {
                if (_isSettingsMenuOpen)
                {
                    ContinueGame();
                    CloseSettingsMenu();
                }
                else
                {
                    PauseGame();
                    OpenSettingsMenu();
                }
            }
        }

        private void ProcessInput()
        {
            if (_isStartScreenOpen)
            {
                _isStartScreenOpen = false;

                PreferToStart?.Invoke();
                StartGame();
            }

            if (_isGameOverScreenOpen)
            {
                PreferToRestart?.Invoke();
                StartGame();
            }
        }

        private void OpenSettingsMenu()
        {
            _isSettingsMenuOpen = true;
            PreferToOpenSettingsMenu?.Invoke();
        }

        private void CloseSettingsMenu()
        {
            _isSettingsMenuOpen = false;
            PreferToCloseSettingsMenu?.Invoke();
        }

        private void OnGameOver()
        {
            PauseGame();
            _scoreCounter.Reset();
            _isGameOverScreenOpen = true;
            GameOver?.Invoke();
        }

        private void StartGame()
        {
            ContinueGame();
            _isGameOverScreenOpen = false;
            _bat.Reset();
            _spawner.Reset();
        }
        
        private void PauseGame()
        {
            Time.timeScale = 0;
        }

        private void ContinueGame()
        {
            Time.timeScale = 1;
        }
    }
}