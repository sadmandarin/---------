using Expedition;
using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationTst : MonoBehaviour
{
    [SerializeField] private LeanPhrase _leanPhrase;
    [SerializeField] private Text _text;
    [SerializeField] private float _testNumber;

    private void Update()
    {
        LevelController levelController = GameObject.Find("LevelControlle").GetComponent<LevelController>();

        string localizedText = LeanLocalization.GetTranslationText(_leanPhrase.name);
        _text.text = string.Format(localizedText, levelController.GetCurrentLevel(0));
    }
}
