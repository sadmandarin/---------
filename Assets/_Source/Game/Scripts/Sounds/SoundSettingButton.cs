using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    public class SoundSettingButton : MonoBehaviour
    {
        [SerializeField] private Sprite _withSound;
        [SerializeField] private Sprite _withoutSound;
        [SerializeField] private Image _settingImage;
        [SerializeField] private Image _borderImage;
        [SerializeField] private Sprite _withSoundBorder;
        [SerializeField] private Sprite _withoutSoundBorder;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(ToggleSound);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ToggleSound);
        }

        private void Start()
        {
            SetUp();
        }
        public void ToggleSound()
        {
            int sound = PlayerPrefs.GetInt("Sound");
            PlayerPrefs.SetInt("Sound", sound == 0 ? 1 : 0);
            SetUp();
        }

        private void SetUp()
        {
            bool withSound = PlayerPrefs.GetInt("Sound") == 0;
            if (withSound)
            {
                SoundManager.Instance.TurnOnSound();
                _settingImage.sprite = _withSound;
                _borderImage.sprite = _withSoundBorder;
            }
            else
            {
                SoundManager.Instance.TurnOffSound();
                _settingImage.sprite = _withoutSound;
                _borderImage.sprite = _withoutSoundBorder;
            }
        }
    }
}