using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class TargetSelectionStat : MonoBehaviour
    {
        [SerializeField] private Text _text;

        internal void Set(TypeOfTargetSelection targetSelection)
        {
            _text.text = LeanLocalization.GetTranslationText(targetSelection.ToString());
        }
    }
}