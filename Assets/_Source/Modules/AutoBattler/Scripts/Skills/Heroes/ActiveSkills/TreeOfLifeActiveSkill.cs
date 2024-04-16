using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class TreeOfLifeActiveSkill : ActiveHeroBaseSkill
    {
        [SerializeField] private float _range;
        [SerializeField] private ParticleSystem _activeSkillParticle;
        [SerializeField] private TreeOfLifeActiveSkillLevels[] _levels;
        [SerializeField] private Transform _heroPosition;

        private Faction _selfFaction;
        private TreeOfLifeActiveSkillLevels _selectedLevel;

        internal override void OnSkillActivated(int level, Faction selfFaction)
        {
            _selfFaction = selfFaction;
            _selectedLevel = _levels[level];
            Instantiate(_activeSkillParticle, _heroPosition.position, Quaternion.identity);
            SphereOverlapper.FindUnitsInsideSphereAndPerformAction(_range, _heroPosition.position, HealUnit);
        }

        private void HealUnit(AutoBattlerUnit unit)
        {
            if (unit.Faction == _selfFaction)
            {
                unit.Heal(BattleReportID, _selectedLevel.AmountToHeal);
            }
        }
    }

    [Serializable]
    internal struct TreeOfLifeActiveSkillLevels
    {
        public float AmountToHeal;
    }
}
