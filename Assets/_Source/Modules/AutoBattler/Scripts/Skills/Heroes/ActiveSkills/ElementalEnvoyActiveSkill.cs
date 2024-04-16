using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class ElementalEnvoyActiveSkill : ActiveHeroBaseSkill
    {
        [SerializeField] private Transform _heroPosition;
        [SerializeField] private AutoBattlerUnit _selfUnit;
        [SerializeField] private ElementEnvoyActiveSkillBurn _skillPrefab;
        [SerializeField] private float _range;
        [SerializeField] private ElementalEnvoySkillLevelsData[] _skillLevels;

        private AutoBattlerUnit _target;

        internal override void OnSkillActivated(int level, Faction selfFaction)
        {
            _target = _selfUnit.Target;
            var skill = Instantiate(_skillPrefab, _target.transform.position, Quaternion.identity);
            skill.Init(_skillLevels[level].DamageOverTime, _target.Faction, BattleReportID);
            SphereOverlapper.FindUnitsInsideSphereAndPerformAction(_range, _target.transform.position, (unit) =>
            {
                if (unit.Faction == _target.Faction)
                    unit.TakeMagicHit(BattleReportID, _skillLevels[level].ImmediateDamage);
            });
        }

        
    }

    [Serializable]
    internal struct ElementalEnvoySkillLevelsData
    {
        public float ImmediateDamage;
        public float DamageOverTime;
    }
}
