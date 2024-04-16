using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class InBattleLevelTxt : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private LeanPhrase _levelPhrase;
        [SerializeField] private CanvasGroup _canvasGroup;

        internal void InitText(int level, bool isMission)
        {
            _canvasGroup.alpha = isMission ? 0 : 1;
            _text.text = LeanLocalization.GetTranslationText(_levelPhrase.name) + " " + level;
        }
    }
}
