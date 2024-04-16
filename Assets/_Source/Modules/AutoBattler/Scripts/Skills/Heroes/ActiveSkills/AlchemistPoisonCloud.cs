using System.Collections;
using UnityEngine;

namespace AutoBattler
{
    internal class AlchemistPoisonCloud : MonoBehaviour
    {
        [SerializeField] private float _poisonRange = 1.5f;
        [SerializeField] private int _poisonTicks = 6;
        [SerializeField] private float _timeForOneTick = 1f;

        private float _damage, _defenseReduction;
        private Faction _factionToHit;
        private BattleReportID _id;

        internal void Init(float damage, float defenseReduction, Faction factionToHit, BattleReportID id)
        {
            _damage = damage;
            _defenseReduction = defenseReduction;
            _factionToHit = factionToHit;
            _id = id;

            StartCoroutine(PoisonOverTime());
            Invoke(nameof(DestroyGameobject), _poisonTicks);
        }

        private IEnumerator PoisonOverTime()
        {
            for (int i = 0; i < _poisonTicks; i++)
            {
                SphereOverlapper.FindUnitsInsideSphereAndPerformAction(_poisonRange, transform.position, PoisonUnit);
                yield return new WaitForSeconds(_timeForOneTick);
            }
            Destroy(gameObject);
        }

        private void PoisonUnit(AutoBattlerUnit unit)
        {
            if (unit.Faction == _factionToHit)
            {
                unit.ApplyModifier(_timeForOneTick, defenseModifier: _defenseReduction);
                unit.TakeMagicHit(_id, _damage);
            }
        }

        private void OnDisable()
        {
            Destroy(gameObject);
        }

        private void DestroyGameobject()
        {
            Destroy(gameObject);
        }
    }
}
