using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class StatModifier : MonoBehaviour
    {
        internal Action<StatModifier> StatModifierEnded;

        [field:SerializeField] public float DamageModifier { get; private set; }
        [field:SerializeField] public float DefenseModifier { get; private set; }
        [field:SerializeField] public float AttackSpeedModifier { get; private set; }
        [field:SerializeField] public float MovementSpeedModifier { get; private set; }

        [SerializeField] private float _duration;

        internal void Init(float duration, float damageModifier = 1, float defenseModifier = 1,
            float attackSpeedModifier = 1, float movementSpeedModifier = 1)
        {
            DamageModifier = damageModifier;
            DefenseModifier = defenseModifier;
            AttackSpeedModifier = attackSpeedModifier;
            MovementSpeedModifier = movementSpeedModifier;
            _duration = duration;
            StartCoroutine(DestroyStatModifier());
        }

        private IEnumerator DestroyStatModifier()
        {
            yield return new WaitForSeconds(_duration);
            StatModifierEnded?.Invoke(this);
            Destroy(this);
        }
    }
}
