using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class OgreSkill : BaseAutoAttackSkill
    {
        [SerializeField] private UnitAnimator _unitAnimator;
        [SerializeField] private float _knockBackStrength;
        [SerializeField] private float _radius;
        [SerializeField] private OgreSkillLevelsData[] _levels;

        private AutoBattlerUnit _target;
        private int _selectedLevel;

        internal override void ActivateSkill(AutoBattlerUnit unit, int levelOfSkill)
        {
            _target = unit;
            _unitAnimator.AnimateSkill(OverLapSphere);
        }

        private void OverLapSphere()
        {
            SphereOverlapper.FindUnitsInsideSphereAndPerformAction(_radius, transform.position, DealDamageAndKnockback);
        }

        private void DealDamageAndKnockback(AutoBattlerUnit unit)
        {
            if (unit.Faction == _target.Faction)
            {
                unit.TakeMagicHit(BattleReportID, _levels[_selectedLevel].Damage);
                unit.transform.DOJump(unit.transform.position + unit.transform.forward * _knockBackStrength * -1, 0.25f, 1, 0.25f);
            }    
        }
    }

    [Serializable]
    public struct OgreSkillLevelsData
    {
        public float Damage;
    }
}
