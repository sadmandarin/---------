using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroPage
{
    internal class HeroDialogModelAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private const string Idle = "Idle";
        internal void HandleSkill()
        {

        }

        internal void HandleSkillEnded()
        {
            _animator.SetTrigger(Idle);
        }
    }
}
