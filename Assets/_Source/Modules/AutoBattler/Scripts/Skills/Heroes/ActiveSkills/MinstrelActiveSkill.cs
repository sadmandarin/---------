using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class MinstrelActiveSkill : ActiveHeroBaseSkill
    {
        [SerializeField] private MinstrelActvieSkillLevelsData[] _levels;
        [SerializeField] private ParticleSystem _skillParticle;
        [SerializeField] private Transform _heroPosition;
        [SerializeField] private float _range;

        private int _selectedLevel;
        private bool _foundEnemy;
        private Faction _selfFaction;

        internal override void OnSkillActivated(int level, Faction selfFaction)
        {
            _foundEnemy = false;
            _selectedLevel = level;
            _selfFaction = selfFaction;
            SphereOverlapper.FindUnitsInsideSphereAndPerformAction(100, _heroPosition.position, FindFirstEnemy);
        }

        private void FindFirstEnemy(AutoBattlerUnit unit)
        {
            if (_foundEnemy) return;

            if (unit.Faction != _selfFaction)
            {
                _foundEnemy = true;
                Instantiate(_skillParticle, unit.transform.position, Quaternion.identity);
                DealDamgeInArea(unit);
            }
        }

        private void DealDamgeInArea(AutoBattlerUnit unit)
        {
            SphereOverlapper.FindUnitsInsideSphereAndPerformAction(_range, unit.transform.position, (unitToHit) =>
            {
                if (unitToHit.Faction != _selfFaction)
                    unitToHit.TakeMagicHit(BattleReportID, _levels[_selectedLevel].Damage);
            });
        }
    }

    [Serializable]
    internal class MinstrelActvieSkillLevelsData
    {
        public float Damage;
    }
}
