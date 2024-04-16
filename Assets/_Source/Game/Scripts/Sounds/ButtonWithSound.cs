using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    public class ButtonWithSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _sound;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(PlaySound);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(PlaySound);
        }

        private void PlaySound()
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound(_sound);
            else
                Debug.LogError("SoundManager is not instantiated");
        }
    }
}
