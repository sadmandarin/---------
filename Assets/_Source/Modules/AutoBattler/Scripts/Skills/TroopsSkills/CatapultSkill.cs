using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class CatapultSkill : BaseAutoAttackSkill
    {
        [SerializeField] private CatapultSkillLevelData[] _levels;
        [SerializeField] private CatapultExplosion _catapultExplosion;
        [SerializeField] private RangedAttacker _rangedAttacker;

        private CatapultSkillLevelData _selectedLevel;
        private Faction _factionToHit;

        internal override void ActivateSkill(AutoBattlerUnit unit, int levelOfSkill)
        {
            _selectedLevel = _levels[levelOfSkill];
            _rangedAttacker.Attack(unit);
            _factionToHit = unit.Faction;
            _rangedAttacker.ProjectileLanded += SpawnDOT;
        }

        private void SpawnDOT(Vector3 position)
        {
            var catapultExplosion = Instantiate(_catapultExplosion, position, Quaternion.identity);
            catapultExplosion.Init(_selectedLevel.Range, _selectedLevel.DamageOverTime, _selectedLevel.Duration, _factionToHit, BattleReportID);
            _rangedAttacker.ProjectileLanded -= SpawnDOT;
        }
    }

    [Serializable]
    public struct CatapultSkillLevelData
    {
        public float DamageOverTime;
        public float Range;
        public float Duration;
    }
}
