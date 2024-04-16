using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class EngineerActiveSkill : ActiveHeroBaseSkill
    {
        [SerializeField] private AutoBattlerUnit _unit;
        [SerializeField] private float _distanceOfRay;
        [SerializeField] private float _explosionRange;
        [SerializeField] private Transform _heroPosition;
        [SerializeField] private MachinistActiveSkillLevelsData[] _skillLevels;
        [SerializeField] private ParticleSystem _rayParticle;
        [SerializeField] private ParticleSystem _explosionParticle;
        [SerializeField] private float _timeToSpawnExplosion = 0.5f;

        private AutoBattlerUnit _unitToHit;
        private int _selectedLevel;
        private Vector3 _explosionSpawnPosition;

        internal override void OnSkillActivated(int level, Faction selfFaction)
        {
            _selectedLevel = level;
            _unitToHit = _unit.Target;

            var ray = new Ray(_heroPosition.position, _unitToHit.transform.position);
            var rayParticle = Instantiate(_rayParticle, _heroPosition.parent);
            //rayParticle.transform.eulerAngles = new Vector3(-90, 90 - _heroPosition.parent.eulerAngles.y, -90);
            _explosionSpawnPosition = _heroPosition.transform.position + _heroPosition.transform.forward * _distanceOfRay;

            RaycastHit[] hits = Physics.RaycastAll(ray, _distanceOfRay);
            foreach (var hit in hits)
            {
                if (hit.transform.TryGetComponent(out AutoBattlerUnit unit))
                {
                    if (unit.Faction != selfFaction)
                    {
                        unit.TakeMagicHit(BattleReportID, _skillLevels[_selectedLevel].DamageOnRay);
                    }
                }
            }
            Invoke("SpawnExplosionAndDealDamage", _timeToSpawnExplosion);
        }

        private void SpawnExplosionAndDealDamage()
        {
            var explosionParticle = Instantiate(_explosionParticle, _explosionSpawnPosition, Quaternion.identity);
            SphereOverlapper.FindUnitsInsideSphereAndPerformAction(_explosionRange, explosionParticle.transform.position, (unit) =>
            {
                if (unit.Faction == _unitToHit.Faction)
                    unit.TakeMagicHit(BattleReportID, _skillLevels[_selectedLevel].DamageAfter);
            });
        }
    }

    [Serializable]
    internal struct MachinistActiveSkillLevelsData
    {
        public float DamageOnRay;
        public float DamageAfter;
    }
}
