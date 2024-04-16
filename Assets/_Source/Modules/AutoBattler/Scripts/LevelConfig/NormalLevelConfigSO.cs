using AutoBattler;
using PersistentData;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/NormalLevelConfig")]
public class NormalLevelConfigSO : LevelConfigBaseSO
{
    [SerializeField] private BattleTerrain _terrain;
    [SerializeField] private LevelsHolderSO _levelsHolder;
    [SerializeField] private BattleDifficultyVariable _difficulty;
    [SerializeField] private BattleDifficultyConfig _config;

    internal override BattleTerrain GetTerrain()
    {
        return _config.GetDifficultyData(_difficulty.Value).Terrain;

    }


    internal override LevelsHolderSO GetLevelsHolder() => _levelsHolder;

    public override bool IsNextLevelAvailable()
    {
        return true;
    }
}



