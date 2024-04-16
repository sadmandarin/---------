using PersistentData;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class EnemyFormationSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _enemyParent;
        [SerializeField] private AutoBattlerUnit[] _enemyPrefabs;
        [SerializeField] private float _spacingBetweenUnits;
        [SerializeField] private BattleDifficultyConfig _difficultyConfig;
        [SerializeField] private LevelVariable _mainLevel;

        internal List<AutoBattlerUnit> SpawnEnemyFormation(EnemyFormation enemyFormation, bool isMission)
        {
            List<AutoBattlerUnit> spawnedUnits = new List<AutoBattlerUnit>();
            for (int i = enemyFormation.Rows.Length - 1; i >= 0 ; i--)
            {
                var currentString = enemyFormation.Rows[i];
                var stringSplit = currentString.Split(',');
                var middlePointOfRows = enemyFormation.Rows.Length / 2;
                for (int j = 0; j < stringSplit.Length ; j++)
                {
                    if (string.IsNullOrEmpty(stringSplit[j]))
                        continue;
                    else
                    {
                        int indexOfUnit = int.Parse(stringSplit[j]);
                        var prefabToSpawn = _enemyPrefabs[Mathf.Clamp(indexOfUnit, 1, _enemyPrefabs.Length - 1)];
                        var spawnedPrefab = Instantiate(prefabToSpawn, _enemyParent);
                        var xStartPosition = _spacingBetweenUnits * -middlePointOfRows;
                        spawnedPrefab.transform.localPosition = new Vector3(xStartPosition + i *_spacingBetweenUnits, 0, 0 - j * _spacingBetweenUnits);
                        spawnedPrefab.MakeUnitAnEnemy();
                        spawnedPrefab.SetLevel(isMission ? 1 : _difficultyConfig.GetLevelOfEnemyTroops());
                        if (isMission == false)
                        {
                            spawnedPrefab.SetRewardForKilling(_difficultyConfig.GetRewardForKilling());
                        }
                        if (_mainLevel.Value <= 2)
                            spawnedPrefab.MakeDamageZero();
                        spawnedUnits.Add(spawnedPrefab);
                    }
                }
            }
            return spawnedUnits;
        }
    }
}
