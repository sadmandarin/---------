using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class AttackDistanceAndTypeStat : MonoBehaviour
    {
        [SerializeField] private Text _attackDistanceText;
        [SerializeField] private Image _attackDistanceImage;
        [SerializeField] private Sprite _shortAttackDistanceSprite;
        [SerializeField] private Sprite _infinityAttackDistanceSprite;
        [SerializeField] private Text _attackTypeText;
        [SerializeField] private Image _attackTypeImage;
        [SerializeField] private Sprite _meleeAttackTypeSprite;
        [SerializeField] private Sprite _rangedAttackTypeSprite;

        private const string MeleeCombat = "MeleeCombat";
        private const string RangedCombat = "RangedCombat";

        internal void Set(bool isMelee, TypeofDistanceOfAttack distanceOfAttack)
        {
            _attackDistanceText.text = Lean.Localization.LeanLocalization.GetTranslationText(distanceOfAttack.ToString());
            _attackDistanceImage.sprite = isMelee ? _shortAttackDistanceSprite : _infinityAttackDistanceSprite;

            _attackTypeText.text = isMelee ? Lean.Localization.LeanLocalization.GetTranslationText(MeleeCombat) :
                                                 Lean.Localization.LeanLocalization.GetTranslationText(RangedCombat);
            _attackTypeImage.sprite = isMelee ? _meleeAttackTypeSprite : _rangedAttackTypeSprite;

        }
    }
}