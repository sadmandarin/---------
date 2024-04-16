using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class FrostEarlActiveSkill : ActiveHeroBaseSkill
    {
        [SerializeField] private FrostEarlActiveSkillsLevelData[] _levelsData;
        [SerializeField] private Transform _heroPosition;
        [SerializeField] private float _explosionRange;

        private const float Radius = 20;

        private FrostEarlActiveSkillsLevelData _skillData;
        private Faction _selfFaction;
        private int _numberOfUnitsFound = 0;

        internal override void OnSkillActivated(int level, Faction selfFaction)
        {
            _skillData = _levelsData[level];
            _selfFaction = selfFaction;
            _numberOfUnitsFound = 0;
            
            SphereOverlapper.FindUnitsInsideSphereAndPerformAction(Radius, _heroPosition.position, SkillAction);
        }

        private void SkillAction(AutoBattlerUnit unitToAffect)
        {
            if (unitToAffect.Faction == _selfFaction)
            {
                unitToAffect.AddInvincibleBarrier(_skillData.NumberOfReflectedAttacks, OnBarrierBroken);
                _numberOfUnitsFound++;
                if (_numberOfUnitsFound >= _skillData.NumberOfUnitToAffect)
                    return;
            }
        }

        private void OnBarrierBroken()
        {
            BattleReportID id = GetComponent<BattleReportView>().ID;
            var unitsInsideSphere = Physics.OverlapSphere(_heroPosition.position, _explosionRange);
            foreach (var unit in unitsInsideSphere)
            {
                if (unit.TryGetComponent(out AutoBattlerUnit unitToAffect))
                {
                    if (unitToAffect.Faction != _selfFaction)
                    {
                        unitToAffect.TakePhysicalHit(id, _skillData.DamageOfBarrierExploding);
                    }
                }
            }
        }
    }

    [Serializable]
    internal struct FrostEarlActiveSkillsLevelData
    {
        public int NumberOfUnitToAffect;
        public int NumberOfReflectedAttacks;
        public float DamageOfBarrierExploding;
    }
}
