using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class JungleHunterSkillAnimation : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _smokeParticle;
        [SerializeField] private ParticleSystem _strikeParticle;

        internal void SpawnSmoke()
        {
            _smokeParticle.Play();
            _strikeParticle.Play();
        }
    }
}
