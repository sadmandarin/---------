using UnityEngine;

namespace AutoBattler
{
    // Can be used for most passive hero skills that give a buff or debuff to units
    internal class GlobalStatsPassiveSkill : PassiveHeroBaseSkill
    {
        [SerializeField] private DoubleSidedModifier[] _levels;

        private const float GlobalPassiveSkillDuration = 100f; // 100 is a big enough number that
                                                               // the passive skill won't end before the battle ends

        internal override void ActivateSkill(Faction selfFaction, int level, float range, Vector3 startPosition)
        {
            var selectedSkillLevel = _levels[level];
            var hits = Physics.OverlapSphere(startPosition, range);
            Debug.Log("Modifying stats");
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out AutoBattlerUnit unit))
                {
                    if (unit.Faction == selfFaction)
                    {
                        unit.ApplyModifier(GlobalPassiveSkillDuration, selectedSkillLevel.AllyBuffs.DamageModifier, 
                                                                       selectedSkillLevel.AllyBuffs.DefenseModifier,
                                                                       selectedSkillLevel.AllyBuffs.AttackSpeedModifier, 
                                                                       selectedSkillLevel.AllyBuffs.MovementSpeedModifier);

                        if (selectedSkillLevel.AllyBuffs.HealthModifier != 1f)
                            unit.ApplyHealthModifier(selectedSkillLevel.AllyBuffs.HealthModifier);
                    }
                    else
                    {
                        unit.ApplyModifier(GlobalPassiveSkillDuration, selectedSkillLevel.EnemyDebuffs.DamageModifier,
                                                                       selectedSkillLevel.EnemyDebuffs.DefenseModifier,
                                                                       selectedSkillLevel.EnemyDebuffs.AttackSpeedModifier,
                                                                       selectedSkillLevel.EnemyDebuffs.MovementSpeedModifier);

                        if (selectedSkillLevel.EnemyDebuffs.HealthModifier != 1f) 
                            unit.ApplyHealthModifier(selectedSkillLevel.EnemyDebuffs.HealthModifier);
                    }
                }
            }
        }
    }
}
