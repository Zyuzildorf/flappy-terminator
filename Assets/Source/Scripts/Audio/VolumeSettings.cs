using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Source.Scripts.Audio
{
    [RequireComponent(typeof(Slider))]
    public class VolumeSettings : MonoBehaviour
    {
        private const int MinVolume = -80;
        private const int ConvertCoefficient = 20;
        private const string MasterGroupName = "Master";

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Toggler _toggler;
        [SerializeField] private string _mixerGroupName;

        private Slider _slider;
        private float _savedSliderValue;
        private bool _isMuted;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(HandleSliderChanged);
            _toggler.UnMuted += SetMasterVolume;
            _toggler.Muted += MuteMaster;
        }

        private void Start()
        {
            ApplyVolume(_slider.value);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(HandleSliderChanged);
            _toggler.UnMuted -= SetMasterVolume;
            _toggler.Muted -= MuteMaster;
        }

        private void HandleSliderChanged(float value)
        {
            _savedSliderValue = value;
            Debug.Log(_savedSliderValue + "сохранённая громкость");
            if (_mixerGroupName != MasterGroupName || _toggler.IsMuted == false)
            {
                ApplyVolume(value);
            }
        }

        private void ApplyVolume(float value)
        {
            if (value == 0)
            {
                _audioMixer.SetFloat(_mixerGroupName, MinVolume);
            }
            else
            {
                _audioMixer.SetFloat(_mixerGroupName, Mathf.Log10(value) * ConvertCoefficient);
            }
        }

        private void MuteMaster()
        {
            _audioMixer.SetFloat(MasterGroupName, MinVolume);
        }

        private void SetMasterVolume()
        {
            _audioMixer.SetFloat(_mixerGroupName, Mathf.Log10(_savedSliderValue) * ConvertCoefficient);
        }
    }
}