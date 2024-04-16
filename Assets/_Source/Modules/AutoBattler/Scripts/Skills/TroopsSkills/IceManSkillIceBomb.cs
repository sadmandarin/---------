using UnityEngine;

namespace AutoBattler
{
    public class IceManSkillIceBomb : MonoBehaviour
    {
        [SerializeField] private float _hitRadius;

        private float _damage, _attackSpeedReduction, _movementSpeedReduction, _duration;
        private Faction _factionToHit;
        private BattleReportID _id;

        internal void Init(float damage, float attackSpeedReduction, float movementSpeedReduction, float duration, Faction factionToHit,
                           BattleReportID id)
        {
            _damage = damage;
            _attackSpeedReduction = attackSpeedReduction;
            _movementSpeedReduction = movementSpeedReduction;
            _duration = duration;
            _factionToHit = factionToHit;
            _id = id;
            Explode();
        }

        internal void Explode()
        {
            var hits = Physics.OverlapSphere(transform.position, _hitRadius);
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out AutoBattlerUnit unit))
                {
                    if (unit.Faction == _factionToHit)
                    {
                        unit.TakeMagicHit(_id, _damage);
                        unit.ApplyModifier(_duration, damageModifier: _attackSpeedReduction, movementSpeedModifier: _movementSpeedReduction);
                    }
                }
            }
        }
    }
}
