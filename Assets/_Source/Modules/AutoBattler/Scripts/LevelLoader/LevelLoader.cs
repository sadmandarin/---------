using PersistentData;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class LevelLoader : MonoBehaviour
    {
        [SerializeField] private IntVariableSO _currentLevel;
        
        [SerializeField] private EnemyFormationSpawner _enemyFormationSpawner;
        [SerializeField] private BattleTerrainInitializer _terrainInitializer;

        private LevelConfigBaseSO _levelConfig;
        private LevelFileReader _levelReader = new LevelFileReader();

        internal void Init(LevelConfigBaseSO levelConfig)
        {
            _levelConfig = levelConfig;
            SpawnTerrain();
        }

        private void SpawnTerrain()
        {
            var terrain = _levelConfig.GetTerrain();
            _terrainInitializer.InitTerrain(terrain);
        }

        internal void ClearTerrain()
        {
            _terrainInitializer.ClearTerrain();
        }

        [ContextMenu(nameof(SpawnEnemies))]
        internal List<AutoBattlerUnit> SpawnEnemies()
        {
            var levelsHolder = _levelConfig.GetLevelsHolder();
            var enemyFormation = _levelReader.ReadLevelFile(levelsHolder, _levelConfig.CurrentLevel);
            return _enemyFormationSpawner.SpawnEnemyFormation(enemyFormation, _levelConfig.IsMission);
        }
    }
}
