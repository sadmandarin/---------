using PersistentData;
using System;
using System.Linq;
using UnityEngine;

namespace AutoBattler
{
    [CreateAssetMenu(menuName = "BattleDifficulty/Config")]
    internal class BattleDifficultyConfig : ScriptableObject
    {
        [field: SerializeField] internal Sprite InactiveButtonSprite { get; private set; }

        [SerializeField] private BattleDifficultyVariable _value;
        [SerializeField] private BattleDifficultyData[] _battleDifficulties;
        

        internal BattleDifficultyData GetDifficultyData(BattleDifficulty difficulty)
        {
            return _battleDifficulties.First(n => n.Difficulty == difficulty);
        }

        internal int GetLevelOfEnemyTroops()
        {
            return _battleDifficulties.First(n => n.Difficulty == _value.Value).LevelOfUnits;
        }

        internal int GetRewardForKilling()
        {
            return _battleDifficulties.First(n => n.Difficulty == _value.Value).RewardForKilling;
        }
    }

    [Serializable]
    internal struct BattleDifficultyData
    {
        public BattleDifficulty Difficulty;
        public int LevelOfUnits;
        public int RewardForKilling;
        public BattleTerrain Terrain;
        public Sprite Skull;
        public Sprite BgImage;
    }
}
