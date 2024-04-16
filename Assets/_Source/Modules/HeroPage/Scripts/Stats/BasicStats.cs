using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class BasicStats : MonoBehaviour
    {
        [SerializeField] private Text _atkText;
        [SerializeField] private Text _atkSpeedText;
        [SerializeField] private Text _defenseText;
        [SerializeField] private Text _speedText;
        [SerializeField] private Text _healthText;

        internal void SetStats(float attack, float health, float defense, float speed, float attackSpeed)
        {
            _atkText.text = attack.ToString();
            _healthText.text = health.ToString();
            _defenseText.text = defense.ToString();
            _speedText.text = speed.ToString();
            _atkSpeedText.text = attackSpeed.ToString();
        }
    }
}