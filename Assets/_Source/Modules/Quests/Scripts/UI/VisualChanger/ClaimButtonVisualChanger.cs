using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    internal class ClaimButtonVisualChanger : VisualChanger
    {
        [SerializeField] private Image _buttonBackground;
        [SerializeField] private Text _text;
        [SerializeField] private LeanPhrase _activePhrase;
        [SerializeField] private LeanPhrase _inactivePhrase;
        internal override void ToggleVisual(bool isActive)
        {
            _buttonBackground.enabled = isActive;
            _text.text = isActive ? LeanLocalization.GetTranslationText(_activePhrase.name) : 
                                    LeanLocalization.GetTranslationText(_inactivePhrase.name);
        }
    }
}
