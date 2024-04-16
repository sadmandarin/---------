using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    internal class AutoFightAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject _swords, _effect;
        [SerializeField] private Animator _animator;
        [SerializeField] private Image[] _swordsForAnimation;

        private const string Animation = "Anim";
        private const string Idle = "Idle";

        internal void PlayAnimation()
        {
            _swords.SetActive(false);
            _effect.SetActive(true);
            _animator.Play(Animation, -1, 0);
            foreach (var sword in _swordsForAnimation)
            {
                sword.color = Color.white;
            }
        }

        internal void StopAnimation()
        {
            _swords.SetActive(true);
            _effect.SetActive(false);
            _animator.Play(Idle);
            foreach (var sword in _swordsForAnimation)
            {
                sword.color = new Color(1,1,1,0);
            }
        }

    }
}
