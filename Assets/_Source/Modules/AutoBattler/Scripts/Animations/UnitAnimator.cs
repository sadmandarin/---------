using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class UnitAnimator : MonoBehaviour
    {
        internal Action AttackAnimationFinished;

        [SerializeField] private Animator _animator;

        private const string Attack = "Attack";
        private const string Run = "Move";
        private const string GameOver = "Victory";
        private const string Idle = "Idle";
        private const string Skill = "Skill";

        private bool _stillInAttackAnimation;
        private Action _attackAction;
        private Action _skillAction;

        internal void AnimateAttack(Action actionAfterAnimationIsDone)
        {
            if (_stillInAttackAnimation)
                return;
            _attackAction = actionAfterAnimationIsDone;
            _animator.SetTrigger(Attack);
            _stillInAttackAnimation = true;
        }

        internal void ResetAttack()
        {
            _animator.ResetTrigger(Attack);
        }

        internal void AnimateRun()
        {
            _animator.SetTrigger(Run);
        }

        internal void AnimateVictory()
        {
            _animator.SetTrigger(GameOver);
        }

        // Used in Animator Event
        internal void HandleAttack()
        {
            AttackAnimationFinished?.Invoke();
            _attackAction.Invoke();
            _animator.ResetTrigger(Attack);
            _stillInAttackAnimation = false;
        }

        internal void AnimateSkill(Action actionAfterAnimationIsDone)
        {
            _skillAction = actionAfterAnimationIsDone;
            _animator.SetTrigger(Skill);
            _stillInAttackAnimation = true;
        }

        // Used in Animator Event
        internal void HandleSkill()
        {
            //if (_stillInAttackAnimation)
            //    return;

            _skillAction.Invoke();
            _animator.ResetTrigger(Skill);
            
        }

        // Used in Animator Event
        internal void HandleSkillEnded()
        {
            _stillInAttackAnimation = false;
        }
    }
}
