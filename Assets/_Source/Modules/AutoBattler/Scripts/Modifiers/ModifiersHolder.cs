using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoBattler
{
    public class ModifiersHolder : MonoBehaviour
    {
        internal Action ModifiersChanged;

        internal float TotalAttackModifer => _statModifiers.Aggregate(1f, (a, b) => a * b.DamageModifier);
        internal float TotalAttackSpeedModifier => _statModifiers.Aggregate(1f, (a, b) => a * b.AttackSpeedModifier);
        internal float TotalMovementSpeedModifier => _statModifiers.Aggregate(1f, (a, b) => a * b.MovementSpeedModifier);
        internal float TotalDefenseMOdifier => _statModifiers.Aggregate(1f, (a, b) => a * b.DefenseModifier);

        [SerializeField] private List<StatModifier> _statModifiers;
            

        internal void AddModifier(float duration, float damageModifier = 1, float defenseModifier = 1,
                float attackSpeedModifier = 1, float movementSpeedModifier = 1)
            {
                StatModifier statModifier = gameObject.AddComponent<StatModifier>();
                statModifier.Init(duration, damageModifier, defenseModifier, attackSpeedModifier, movementSpeedModifier);
                _statModifiers.Add(statModifier);
                ModifiersChanged?.Invoke();
                statModifier.StatModifierEnded += HandleModifierEnded;
            }

            private void HandleModifierEnded(StatModifier modifier)
            {
                modifier.StatModifierEnded -= HandleModifierEnded;
                _statModifiers.Remove(modifier);
                ModifiersChanged?.Invoke();
            }
    }
}
