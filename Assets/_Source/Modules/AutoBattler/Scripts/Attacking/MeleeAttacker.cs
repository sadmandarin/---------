using UnityEngine;

namespace AutoBattler
{
    internal class MeleeAttacker : Attacker
    {
        [SerializeField] private UnitAnimator _animator;
        internal override void Attack(AutoBattlerUnit target)
        {
            _animator.AnimateAttack(() => HitTargetAfterAnimation(target));
        }

        private void HitTargetAfterAnimation(AutoBattlerUnit target)
        {
            target.TakePhysicalHit(BattleReportID, Damage);
        }
    }
}
