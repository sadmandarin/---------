using System;
using UnityEngine;

namespace AutoBattler
{
    internal class MeleeAreaAttacker : Attacker
    {
        [SerializeField] private ParticleSystem _hitParticle;
        [SerializeField] private UnitAnimator _unitAnimator;
        [SerializeField] private bool _magicDamage;

        private Faction _targetFaction;

        internal override void Attack(AutoBattlerUnit target)
        {
            _targetFaction = target.Faction;
            _unitAnimator.AnimateAttack(OnAttackAnimated);
        }

        private void OnAttackAnimated()
        {
            SphereOverlapper.FindUnitsInsideSphereAndPerformAction(HitRadius, transform.position, DealDamageInArea);
            _hitParticle.Play();
        }

        private void DealDamageInArea(AutoBattlerUnit unit)
        {
            if (unit.Faction == _targetFaction)
            {
                if (_magicDamage)
                    unit.TakeMagicHit(BattleReportID, Damage);
                else
                    unit.TakePhysicalHit(BattleReportID, Damage);
            }
        }
    }
}
