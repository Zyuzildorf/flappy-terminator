using System;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : Window
{
    [SerializeField] private Button _actionButton;

    public event Action PlayButtonClicked;
    
    private void OnEnable()
    {
        _actionButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _actionButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        PlayButtonClicked?.Invoke();
    }
}
