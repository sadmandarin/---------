using System;
using UnityEngine;

namespace AutoBattler
{
    internal class Health : MonoBehaviour
    {
        internal Action HealthReachedZero;
        internal Action<float, float> HealthChanged;
        internal float DefenseStat => _defense;

        private float _maxHealth, _currentHealth;
        private float _defense;
        private bool _isHeroUnit;
        private float _shieldAmount;    
        private float _healthModifier = 1;
        private int _invincibleBarrierHits;
        private Action _onInvincibleBarrierBroken;
        private GameObject _shieldEffect;

        internal void Init(float health, float defense, bool isHero)
        {
            _maxHealth = health * _healthModifier;
            _currentHealth = _maxHealth;
            _defense = defense;
            _isHeroUnit = isHero;
        }

        internal void ApplyHealthModifier(float healthModifier)
        {
            _healthModifier = healthModifier;
            if (_currentHealth != 0)
            {
                _currentHealth *= healthModifier;
                _maxHealth *= healthModifier;
            }
        }

        internal void TakeDamage(float attack, out float effectiveDamage, out float damageBlocked, bool ignoreDefense = false)
        {
            effectiveDamage = 0;
            damageBlocked = 0;

            if (_currentHealth <= 0)
                return;

            if (_invincibleBarrierHits > 0)
            {
                _invincibleBarrierHits -= 1;
                if (_invincibleBarrierHits == 0)
                {
                    _onInvincibleBarrierBroken?.Invoke();
                }
                return;
            }

            float damage = ignoreDefense ? attack : CalculateDamage(attack);
            damageBlocked = ignoreDefense ? 0 : _defense;

            if (_shieldAmount > 0)
            {
                var shieldAmountBeforeHit = _shieldAmount;
                _shieldAmount -= damage;
                if (_shieldAmount < 0)
                    _shieldAmount = 0;
                damage -= shieldAmountBeforeHit;

                if (_shieldAmount == 0) _shieldEffect.SetActive(false);

                if (damage <= 0)
                    return;
            }

            var healthBeforeAttack = _currentHealth;
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
            effectiveDamage = healthBeforeAttack - _currentHealth;

            HealthChanged?.Invoke(_currentHealth, _maxHealth);

            if (_currentHealth <= 0)
            {
                HealthReachedZero?.Invoke();
            }
        }

        internal void AddInvincibleBarrier(int numberOfHits, Action onInvincibleBarrierBroken)
        {
            _invincibleBarrierHits = numberOfHits;
            _onInvincibleBarrierBroken = onInvincibleBarrierBroken;
        }

        internal void HealPercentOfTotalHealth(float percentToHeal, out float amountHealed)
        {
            var amountToHeal = _maxHealth * percentToHeal;
            Heal(amountToHeal, out amountHealed);
        }

        internal void Heal(float amountToHeal, out float amountHealed)
        {
            var healthBefore = _currentHealth;
            _currentHealth += amountToHeal;
            if (_currentHealth >= _maxHealth)
                _currentHealth = _maxHealth;
            amountHealed = _currentHealth - healthBefore;
        }

        internal void AddShield(float shieldAmount, GameObject shieldEffect)
        {
            _shieldAmount += shieldAmount;
            if (_shieldEffect == null)
                _shieldEffect = Instantiate(shieldEffect, transform);
            else
                _shieldEffect.SetActive(true);
        }

        internal void SetDefense(float value)
        {
            _defense = value;
        }

        private float CalculateDamage(float attack)
        {
            float calulatedDamage = 0;
            if (_isHeroUnit == false)
            {
                calulatedDamage = attack - _defense;
            }
            else
            {
                calulatedDamage = (attack * attack) / (attack + _defense);
            }
            if (calulatedDamage < 0)
                calulatedDamage = 0;

            return calulatedDamage;
        }
    }
}
