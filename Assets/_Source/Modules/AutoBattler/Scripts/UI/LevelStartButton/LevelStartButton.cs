using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class LevelStartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _buttonText;
        [SerializeField] private LeanPhrase _levelPhrase, _inBattlePhrase;

        private Action _action;

        internal void Init(int level, bool isMisson, Action action)
        {
            gameObject.SetActive(true);
            _action = action;
            if (isMisson)
                _buttonText.text = Lean.Localization.LeanLocalization.GetTranslationText(_inBattlePhrase.name);
            else
                _buttonText.text = Lean.Localization.LeanLocalization.GetTranslation("Level").Data.ToString() + " " + level;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _action.Invoke();
            gameObject.SetActive(false);
        }
    }
}
