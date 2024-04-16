using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "ExtraReward/MainLevelsConfig")]
    public class MainLevelsExtraRewardConfig : ExtraRewardsBaseConfig
    {
        [SerializeField] private ExtraRewardBase _coinsReward, _gemsReward, _troopChestReward, _heroChestReward;
        [SerializeField] private List<MainLevelRewardData> _rewardsList;

        public override ExtraRewardBase[] GetRewardByLevel(int level)
        {
            ExtraRewardBase[] result = new ExtraRewardBase[1];
            int maxPredefinedReward = _rewardsList.Max(x => x.Level);

            if (_rewardsList.Any(n => n.Level == level))
            {
                result[0] = _rewardsList.First(n => n.Level == level).ExtraReward;
                return result;
            }

            if (maxPredefinedReward > level)
                return result;

            if (level % 20 == 0)
                result[0] = _heroChestReward;
            else if (level % 10 == 0)
                result[0] = _troopChestReward;
            else if (level % 5 == 0 && level % 15 == 0)
                result[0] = _gemsReward;
            else if (level % 5 == 0)
                result[0] = _coinsReward;
            else
                result[0] = null;
            return result;
        }

        public override int GetQuantityForReward(ExtraRewardBase reward, int level)
        {
            // Player gets one reward type every 20 levels
            int whichTimePlayerEncountersThisReward = level / 20;
            return reward.StartingNumber + reward.CommonDifference * whichTimePlayerEncountersThisReward;
        }
    }

    [Serializable]
    internal struct MainLevelRewardData
    {
        public int Level;
        public ExtraRewardBase ExtraReward;
    }
}
