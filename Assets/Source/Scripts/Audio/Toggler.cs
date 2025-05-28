using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class Toggler : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _masterMixer;

    private bool _isMuted;
    private Toggle _toggle;

    public bool IsMuted => _isMuted;
    public event Action Muted; 
    public event Action UnMuted; 
    
    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(ToggleVolume);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(ToggleVolume);
    }

    private void ToggleVolume(bool isOn)
    {
        if (isOn)
        {
            _isMuted = false;
            UnMuted?.Invoke();
        }
        else
        {
            _isMuted = true;
            Muted?.Invoke();
        }
    }
}