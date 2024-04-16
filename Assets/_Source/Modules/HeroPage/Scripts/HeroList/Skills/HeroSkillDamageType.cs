using Lean.Localization;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class HeroSkillDamageType : MonoBehaviour
    {
        [SerializeField] private Image _leftImage, _rightImage;
        [SerializeField] private Sprite _magicArrow, _physicalArrow;
        [SerializeField] private LeanPhrase _magicDamage, _physicalDamage;
        [SerializeField] private Color _magicDamageColor, _physicalDamageColor;
        [SerializeField] private Text _text;

        internal void SetUp(SkillDamageType damageType)
        {
            if (damageType == SkillDamageType.None)
            {
                gameObject.SetActive(false);
                return;
            }
            bool isMagical = damageType == SkillDamageType.Magical;
            _text.text = LeanLocalization.GetTranslationText(isMagical ? _magicDamage.name : _physicalDamage.name);
            _leftImage.sprite = isMagical ? _magicArrow : _physicalArrow;
            _rightImage.sprite = isMagical ? _magicArrow : _physicalArrow;
            _text.color = isMagical ? _magicDamageColor : _physicalDamageColor;
        }
    }

    [Serializable]
    internal enum SkillDamageType
    {
        None,
        Physical,
        Magical
    }
}
