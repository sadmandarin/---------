using PersistentData;
using Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public abstract class LevelConfigBaseSO : ScriptableObject
    {
        internal int CurrentLevel => _levelVariable.Value;
        internal ExtraRewardsBaseConfig ExtraRewards => _extraRewards;

        public LevelVariable LevelVariable { get => _levelVariable; private set => _levelVariable = value; }
        public bool CanSpawnTroops { get => _canSpawnTroops; private set => _canSpawnTroops = value; }
        public bool IsMission { get => _isMission; private set => _isMission = value; }
        public abstract bool IsNextLevelAvailable();

        [SerializeField] private LevelVariable _levelVariable;
        [SerializeField] private ExtraRewardsBaseConfig _extraRewards;
        [SerializeField] private QuestsCompleter _questsCompleter;
        [SerializeField] private QuestItemDescriptionSO _quest;
        [SerializeField] private bool _canSpawnTroops;
        [SerializeField] private bool _isMission;

        internal abstract BattleTerrain GetTerrain();

        internal abstract LevelsHolderSO GetLevelsHolder();

        internal void IncreaseLevel()
        {
            if (_isMission)
                YandexSDK.YandexMetrika.Event("lvl" + _levelVariable.Value);

            _levelVariable.Value += 1;

            if (_questsCompleter != null && _quest != null)
                _questsCompleter.CompleteQuest(_quest);
        }
    }
}



