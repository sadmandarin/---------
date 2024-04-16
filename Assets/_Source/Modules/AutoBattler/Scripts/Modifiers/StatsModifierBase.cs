using System;
using UnityEngine;

namespace AutoBattler
{
    [Serializable]
    internal class StatsModifierBase
    {
        public float DamageModifier => _damageModifier;
        public float DefenseModifier => _defenseModifier;
        public float AttackSpeedModifier => _attackSpeedModifier;
        public float MovementSpeedModifier => _movementSpeedModifier;
        public float HealthModifier => _healthModifier;

        [SerializeField] private float _damageModifier = 1f;
        [SerializeField] private float _defenseModifier = 1f;
        [SerializeField] private float _attackSpeedModifier = 1f;
        [SerializeField] private float _movementSpeedModifier = 1f;
        [SerializeField] private float _healthModifier = 1f;
    }
}
