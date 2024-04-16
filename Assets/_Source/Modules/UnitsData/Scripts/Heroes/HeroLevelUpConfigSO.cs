using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitsData
{
    [CreateAssetMenu(menuName = "Heroes/HeroLevelUpConfig")]
    public class HeroLevelUpConfigSO : ScriptableObject
    {
        public int MaxLevel => Levels.Length + 1;
        public int GetShardsForUpgrade(int currentLevel) => Levels[currentLevel - 1].ShardsToUpgrade;
        public int GetMoneyForUpgrade(int currentLevel) => Levels[currentLevel - 1].MoneyToUpgrade;

        [SerializeField] private HeroLevelData[] Levels; 
    }

    [Serializable]
    internal struct HeroLevelData
    {
        public int ShardsToUpgrade;
        public int MoneyToUpgrade;
    }
}
