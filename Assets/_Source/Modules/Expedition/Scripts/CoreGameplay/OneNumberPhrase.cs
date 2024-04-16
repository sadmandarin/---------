using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneNumberPhrase : MonoBehaviour
{
    [SerializeField] private LeanPhrase _leanPhrase;
    [SerializeField] private Text _text;
    [SerializeField] private float _testNumber;

    private void Start()
    {
        string localizedText = LeanLocalization.GetTranslationText(_leanPhrase.name);
        _text.text = string.Format(localizedText, _testNumber);
    }
}
