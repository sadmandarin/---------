using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal abstract class Attacker : MonoBehaviour
    {
        internal bool CanAttack { get; private set; } = true;
        internal int TimesAttacked { get; private set; } = 0;
        internal float AttackStat => Damage;

        protected float AttackSpeed = 0.2f;
        protected float Damage;

        [SerializeField] protected float HitRadius;
        protected BattleReportID BattleReportID { get; private set; }

        internal abstract void Attack(AutoBattlerUnit target);

        internal void StartAttackDelay()
        {
            TimesAttacked += 1;
            StartCoroutine(AttackDelay());
        }

        private IEnumerator AttackDelay()
        {
            CanAttack = false;
            yield return new WaitForSeconds(1 / AttackSpeed);
            CanAttack = true;
        }

        internal void ResetNumberOfAttacks()
        {
            TimesAttacked = 0;
        }

        internal void SetStats(float damage, float attackSpeed, float hitRadius)
        {
            Damage = damage;
            AttackSpeed = attackSpeed;
            HitRadius = hitRadius * 0.3f;
        }

        internal void SetBattleReportID(BattleReportID id)
        {
            BattleReportID = id;
        }
    }
}
