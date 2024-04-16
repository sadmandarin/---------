using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace AutoBattler
{
    internal class InfiniteBattlerRoot : MonoBehaviour
    {
        [SerializeField] private AutoBattlerUnitsManager _unitsManager;
        [SerializeField] private InfiniteBattlerEnemySpawner _enemySpawner;

        private void Awake()
        {
            _unitsManager.UpdateUnitsStats();
            _unitsManager.EnemyUnitDied += HandleEnemyUnitDied;
            _unitsManager.InitUnitsForBattle();
        }

        private void HandleEnemyUnitDied(AutoBattlerUnit unit)
        {
            var spawnedEnemy = _enemySpawner.SpawnEnemy();
            _unitsManager.AddEnemyUnits(new List<AutoBattlerUnit>() { spawnedEnemy });
            spawnedEnemy.UnitDied += _unitsManager.UnitDiedHandler;
            spawnedEnemy.InitUnitTargets(_unitsManager.Units.Where(n => n.Faction == Faction.Player).Select(n => n.transform).ToList());
        }
    }
}
