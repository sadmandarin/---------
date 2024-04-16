using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class DeadlyAssassinsSkill : BaseAutoAttackSkill
    {
        [SerializeField] private UnitAnimator _unitAnimator;
        [SerializeField] private GameObject _skillParticles;
        [SerializeField] private float _range;
        [SerializeField] private DeadlyAssassinsSkillLevel[] _levels;

        private Faction _targetFaction;
        private int _skillLevel;

        internal override void ActivateSkill(AutoBattlerUnit unit, int levelOfSkill)
        {
            _targetFaction = unit.Faction;
            _skillLevel = levelOfSkill;
            Instantiate(_skillParticles, transform.position, Quaternion.identity);
            _unitAnimator.AnimateSkill(() => 
                SphereOverlapper.FindUnitsInsideSphereAndPerformAction(_range, transform.position, DealDamage));
        }

        private void DealDamage(AutoBattlerUnit unit)
        {
            if (unit.Faction == _targetFaction)
            {
                unit.TakePhysicalHit(BattleReportID, _levels[_skillLevel].Damage);
            }
        }
    }

    [Serializable]
    internal struct DeadlyAssassinsSkillLevel 
    {
        public float Damage;
    }
}
