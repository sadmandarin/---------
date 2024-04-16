using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class IceManSkill : BaseAutoAttackSkill
    {
        [SerializeField] private IceManSkillLevelData[] _levels;
        [SerializeField] private IceManSkillIceBomb _iceBombPrefab;
        [SerializeField] private UnitAnimator _unitAnimator;

        private IceManSkillLevelData _selectedLevel;

        internal override void ActivateSkill(AutoBattlerUnit unitToHit, int levelOfSkill)
        {
            _selectedLevel = _levels[levelOfSkill];
            _unitAnimator.AnimateAttack(() => SpawnIceBomb(unitToHit));
        }

        private void SpawnIceBomb(AutoBattlerUnit unitToHit)
        {
            var iceBomb = Instantiate(_iceBombPrefab, unitToHit.transform.position, Quaternion.identity);
            iceBomb.Init(_selectedLevel.Damage, _selectedLevel.AttackSpeedReduction, _selectedLevel.MovementSpeedReduction, 
                         _selectedLevel.Duration, unitToHit.Faction, BattleReportID);
        }
    }

    [Serializable]
    public struct IceManSkillLevelData
    {
        public int Damage;
        public float Duration;
        public float AttackSpeedReduction;
        public float MovementSpeedReduction;
    }
}
