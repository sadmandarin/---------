using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace AutoBattler
{
    public class CatapultExplosion : MonoBehaviour
    {
        [SerializeField] private float _timeToDamage = 1;

        private float _radius;
        private float _damageOverTime;
        private Faction _factionToHit;
        private float _timer;
        private float _duration;
        private BattleReportID _battleReportID;

        internal void Init(float radius, float damageOverTime, float duration, Faction factionToHit, BattleReportID id)
        {
            _radius = radius;
            _damageOverTime = damageOverTime;
            _duration = duration;
            _factionToHit = factionToHit;
            _battleReportID = id;
        }

        private void OnEnable()
        {
            _timer = 0;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _timeToDamage )
            {
                _timer = 0;
                _duration -= _timeToDamage;
                var hits = Physics.OverlapSphere(transform.position, _radius);
                foreach (var hit in hits)
                {
                    if (hit.TryGetComponent(out AutoBattlerUnit unit))
                        if (unit.Faction == _factionToHit)
                            unit.TakePhysicalHit(_battleReportID, _damageOverTime);
                }
            }
            if (_timer >= _duration)
                Destroy(gameObject);

                }
    }
}
