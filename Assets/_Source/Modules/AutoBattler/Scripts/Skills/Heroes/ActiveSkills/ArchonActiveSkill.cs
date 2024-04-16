using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class ArchonActiveSkill : ActiveHeroBaseSkill
    {
        [SerializeField] private Transform _heroPosition;
        [SerializeField] private ArchonActiveSkillLevelsData[] _skillLevels;
        [SerializeField] private MeteorShower _meteorShower;

        private int _selectedLevel;
        private Faction _selfFaction;
        private int _numberOfUnitsHit;

        internal override void OnSkillActivated(int level, Faction selfFaction)
        {
            _selectedLevel = level;
            _selfFaction = selfFaction;
            _numberOfUnitsHit = 0;
            SphereOverlapper.FindUnitsInsideSphereAndPerformAction(20, _heroPosition.position, SpawnParticleAndDealDamage);
        }

        private void SpawnParticleAndDealDamage(AutoBattlerUnit unit)
        {
            if (_numberOfUnitsHit >= _skillLevels[_selectedLevel].NumberOfUnitsToHit)
                return;
            if (unit.Faction != _selfFaction)
            {
                var meteorShower = Instantiate(_meteorShower, unit.transform);
                meteorShower.Init(() => DealDamageToUnit(unit));
                _numberOfUnitsHit++;
            }
        }

        private void DealDamageToUnit(AutoBattlerUnit unit)
        {
            if (unit!= null) unit.TakeMagicHit(BattleReportID, _skillLevels[_selectedLevel].Damage);
        }
    }

    [Serializable]
    internal struct ArchonActiveSkillLevelsData
    {
        public int NumberOfUnitsToHit;
        public float Damage;
    }
}
