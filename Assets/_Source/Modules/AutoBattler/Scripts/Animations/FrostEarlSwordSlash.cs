using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class FrostEarlSwordSlash : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        internal void ActivateSwordSlash()
        {
            _particleSystem.Play();
        }
    }
}
