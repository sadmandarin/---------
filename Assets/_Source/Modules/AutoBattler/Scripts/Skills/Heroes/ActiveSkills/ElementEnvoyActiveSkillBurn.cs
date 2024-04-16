using System.Collections;
using UnityEngine;

namespace AutoBattler
{
    internal class ElementEnvoyActiveSkillBurn : MonoBehaviour
    {
        [SerializeField] private int _timeTicks;
        [SerializeField] private float _timeForOneTick;
        [SerializeField] private float _range;

        private Faction _factionToHit;
        private float _damage;
        private BattleReportID _id;

        internal void Init(float damage, Faction factionToHit, BattleReportID id)
        {
            _factionToHit = factionToHit;
            _damage = damage;
            _id = id;
            StartCoroutine(FindUnitsAndDealDamageOverTime());
        }

        private IEnumerator FindUnitsAndDealDamageOverTime()
        {
            for (int i = 0; i < _timeTicks; i++)
            {
                SphereOverlapper.FindUnitsInsideSphereAndPerformAction(_range, transform.position, DealDamageToUnit);
                yield return new WaitForSeconds(_timeForOneTick);
            }

            Destroy(gameObject);
        }

        private void DealDamageToUnit(AutoBattlerUnit unit)
        {
            if (unit.Faction == _factionToHit)
                unit.TakeMagicHit(_id, _damage);
        }
    }
}
