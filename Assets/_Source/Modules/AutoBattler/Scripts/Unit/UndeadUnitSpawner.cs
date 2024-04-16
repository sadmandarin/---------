using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace AutoBattler
{
    internal class UndeadUnitSpawner : MonoBehaviour
    {
        [SerializeField] private AutoBattlerUnit _undeadSoldier;
        [SerializeField] private AutoBattlerUnitsManager _unitsManager;
        [SerializeField] private float _delayToSpawnUndead;
        [SerializeField] private BattleReportRoot _battleReportRoot;

        internal void SpawnUndead(Vector3 position, int level, Faction faction)
        {
            StartCoroutine(SpawnUndeadCoroutine(position, level, faction));
            
        }

        private IEnumerator SpawnUndeadCoroutine(Vector3 position, int level, Faction faction)
        {
            yield return new WaitForSeconds(_delayToSpawnUndead);
            var unit = Instantiate(_undeadSoldier, position, Quaternion.identity);
            _unitsManager.AddPlayerUnitsWithoutRepeating(new List<GameObject> { unit.gameObject }, level + 1);
            unit.InitUnitStats();
            unit.InitUnitTargets(_unitsManager.Units.Where(n => n.Faction != faction).Select(n => n.transform).ToList());
            if (faction == Faction.Enemy)
                unit.MakeUnitAnEnemy();
            unit.UnitDied += _unitsManager.UnitDiedHandler;
            _battleReportRoot.AddUnitToBattleReport(unit);
        }
    }
}
