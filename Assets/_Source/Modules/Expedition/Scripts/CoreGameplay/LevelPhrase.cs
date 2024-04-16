using Expedition;
using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPhrase : MonoBehaviour
{
    [SerializeField] private LeanPhrase _leanPhrase;
    [SerializeField] private Text _text;

    

    public void UpdateText(int currentLevel)
    {
        string localizedText = LeanLocalization.GetTranslationText(_leanPhrase.name);

        _text.text = string.Format(localizedText, currentLevel);
    }
}