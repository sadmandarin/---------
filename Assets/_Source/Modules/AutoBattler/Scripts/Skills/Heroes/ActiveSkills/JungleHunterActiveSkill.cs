using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class JungleHunterActiveSkill : ActiveHeroBaseSkill
    {
        [SerializeField] private AutoBattlerUnit _selfUnit;
        [SerializeField] private Transform _heroPosition;
        [SerializeField] private float _distanceToTeleport;
        [SerializeField] private ParticleSystem[] _teleportParticles;
        [SerializeField] private float[] _skillLevels;

        internal override void OnSkillActivated(int level, Faction selfFaction)
        {
            var target = _selfUnit.Target;
            _heroPosition.transform.position = target.transform.position + target.transform.forward * -1 * _distanceToTeleport;
            _heroPosition.transform.rotation = Quaternion.RotateTowards(_heroPosition.rotation, target.transform.rotation, 360);
            foreach (var particle in _teleportParticles)
            {
                particle.Play();
            }
            target.TakePhysicalHit(_selfUnit.GetComponent<BattleReportView>().ID, _skillLevels[level]);
        }
    }
}
