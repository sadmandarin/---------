using Lean.Localization;
using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainPage
{
    internal class StartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private LevelVariable _currentLevel;
        [SerializeField] private Text _buttonText;
        [SerializeField] private LeanPhrase _levelPhrase;

        private void OnEnable()
        {
            //SetButtonText(_currentLevel.Value);
            _currentLevel.OnValueChanged += SetButtonText;
        }

        private void OnDisable()
        {
            _currentLevel.OnValueChanged -= SetButtonText;
        }

        private void SetButtonText(int currentLevel)
        {
            _buttonText.text = LeanLocalization.GetTranslationText(_levelPhrase.name) + " " + currentLevel;
        }
    }
}
