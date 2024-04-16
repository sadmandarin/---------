using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class BlackBeardActiveSkill : ActiveHeroBaseSkill
    {
        [SerializeField] private ParticleSystem _skillEffect;
        [SerializeField] private ParticleSystem _buffEffect;
        [SerializeField] private Transform _heroPosition;
        [SerializeField] private BlackBeardActiveSkillLevelsData[] _skillLevels;
        [SerializeField] private float _buffTime = 3;

        private int _selectedLevel;
        private Faction _selfFaction;
        private int _numberOfUnitsBuffed;

        internal override void OnSkillActivated(int level, Faction selfFaction)
        {
            _selectedLevel = level;
            _selfFaction = selfFaction;
            _numberOfUnitsBuffed = 0;
            _skillEffect.Play();
            SphereOverlapper.FindUnitsInsideSphereAndPerformAction(20, _heroPosition.position, ApplyBuffToUnit);
        }

        private void ApplyBuffToUnit(AutoBattlerUnit unit)
        {
            if (_numberOfUnitsBuffed >= _skillLevels[_selectedLevel].NumberOfUnits)
                return;

            if (unit.Faction == _selfFaction)
            {
                unit.ApplyModifier(_buffTime, damageModifier: _skillLevels[_selectedLevel].AttackModifier,
                                              movementSpeedModifier: _skillLevels[_selectedLevel].MovementSpeedModifier);
                _numberOfUnitsBuffed++;
                var buffEffect = Instantiate(_buffEffect, unit.transform);
                Destroy(buffEffect, _buffTime);
            }
        }
    }

    [Serializable]
    internal class BlackBeardActiveSkillLevelsData
    {
        public int NumberOfUnits;
        public float AttackModifier;
        public float MovementSpeedModifier;
    }
}
