using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class AlchemistPassiveSkill : PassiveHeroBaseSkill
    {
        [SerializeField] private AlchemistPassiveSkillLevelsData[] _levels;
        internal override void ActivateSkill(Faction selfFaction, int level, float range, Vector3 startPosition)
        {
            var selectedSkillLevel = _levels[level];
            var hits = Physics.OverlapSphere(startPosition, range);
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out AutoBattlerUnit unit))
                {
                    if (unit.Faction == selfFaction)
                    {
                        unit.ApplyModifier(100, selectedSkillLevel.AllyDamageModifier);
                    }
                    else
                    {
                        unit.GetComponent<RewardForKilling>().ChangeRewardBy((int)selectedSkillLevel.EnemyBonusMoney);
                    }
                }
            }
        }
    }

    [Serializable]
    internal struct AlchemistPassiveSkillLevelsData
    {
        public float AllyDamageModifier;
        public float EnemyBonusMoney;
    }
}
