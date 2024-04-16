using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    public class HeroStatsBasicValues : MonoBehaviour
    {
        [SerializeField] private Text _healthText, _defenseText, _attackText;
        [SerializeField] private Image _attackTypeImage;
        [SerializeField] private Sprite _magicAttackSprite, _physicalAttackSprite;

        internal void Set(float health, float defense, float attack, bool isMagicAttack)
        {
            _healthText.text = health.ToString();
            _defenseText.text = defense.ToString();
            _attackText.text = attack.ToString();
            _attackTypeImage.sprite = isMagicAttack ? _magicAttackSprite : _physicalAttackSprite;
        }
    }
}
