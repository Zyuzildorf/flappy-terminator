using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Bat _bat;
    [SerializeField] private Camera _camera;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private HUD _hud;

    private void OnEnable()
    {
        _bat.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _bat.GameOver -= OnGameOver;
    }
}