using UnityEngine;

namespace AutoBattler
{
    internal class InfiniteBattlerEnemySpawner : MonoBehaviour
    {
        [SerializeField] private float _timeToSpawnAnEnemy;
        [SerializeField] private AutoBattlerUnit _enemyUnitToSpawn;
        [SerializeField] private Transform _enemySpawnPosition;

        internal AutoBattlerUnit SpawnEnemy()
        {
            var enemy = Instantiate(_enemyUnitToSpawn, _enemySpawnPosition);
            enemy.MakeUnitAnEnemy();
            enemy.InitUnitStats();
            return enemy;
        }
    }
}
