using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

public class TwoNumberPhrase : MonoBehaviour
{
    [SerializeField] private LeanPhrase _leanPhrase;
    [SerializeField] private Text _text;
    [SerializeField] private float firstNumber;
    [SerializeField] private float secondNumber;


    private void Start()
    {
        string localizedText = LeanLocalization.GetTranslationText(_leanPhrase.name);
        _text.text = string.Format(localizedText, firstNumber, secondNumber);
    }
}

