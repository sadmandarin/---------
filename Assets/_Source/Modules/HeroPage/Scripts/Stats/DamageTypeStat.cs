using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class DamageTypeStat : MonoBehaviour
    {
        [SerializeField] private Text _damageTypeText;
        [SerializeField] private Image _damageTypeImage;
        [SerializeField] private Sprite _physcialAttackSprite;
        [SerializeField] private Sprite _magicAttackSprite;

        private const string MagicAttack = "MagicAttack";
        private const string PhysicalAttack = "PhysicalAttack";

        internal void Set(bool isMagic)
        {
            _damageTypeImage.sprite = isMagic ? _magicAttackSprite : _physcialAttackSprite;
            _damageTypeText.text = isMagic ? Lean.Localization.LeanLocalization.GetTranslationText(MagicAttack) :
                                               Lean.Localization.LeanLocalization.GetTranslationText(PhysicalAttack);
        }
    }
}