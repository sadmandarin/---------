using AutoBattler;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/LevelsHolderConfig")]
internal class MissionLevelsHolderConfig : ScriptableObject
{
    [SerializeField] private List<MissionLevelsData> LevelsData;

    internal LevelsHolderSO GetLevelsHolderByLevel(int mainLevel)
    {
        if (LevelsData.Any(n => n.LevelRequirement < mainLevel))
        {
            return LevelsData.Last(n => n.LevelRequirement < mainLevel).LevelHolder;
        }
        else
        {
            return LevelsData[LevelsData.Count - 1].LevelHolder;
        }
    }
}

[Serializable]
internal struct MissionLevelsData
{
    public LevelsHolderSO LevelHolder;
    public int LevelRequirement;
}



