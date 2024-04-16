using AutoBattler;
using PersistentData;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/MissionsLevelConfig")]
public class MissionsLevelConfigSO : LevelConfigBaseSO
{
    [SerializeField] private BattleTerrain[] _terrains;
    [SerializeField] private MissionLevelsHolderConfig _levelHolderConfig;
    [SerializeField] private LevelVariable _mainLevelVariable;
    [SerializeField] private IntVariableSO _timesRemaining;

    public override bool IsNextLevelAvailable()
    {
        if (_timesRemaining.Value == 0)
            return false;
        else
            return true;
    }

    internal override LevelsHolderSO GetLevelsHolder()
    {
        return _levelHolderConfig.GetLevelsHolderByLevel(_mainLevelVariable.Value);
    }

    internal override BattleTerrain GetTerrain()
    {
        int indexOfTerrain = Mathf.Clamp((CurrentLevel % 10) - 1, 0, _terrains.Length - 1);
        return _terrains[indexOfTerrain];
    }
}



