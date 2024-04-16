using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "ExtraReward/MissionsConfig")]
    public class MissionsExtraRewardsConfig : ExtraRewardsBaseConfig
    {
        [SerializeField] private List<MissionLevelExtraRewardData> _extraRewardsData;
        public override int GetQuantityForReward(ExtraRewardBase reward, int level)
        {
            return _extraRewardsData[level - 1].Rewards.First(n => n.Reward == reward).Quantity;
        }

        public override ExtraRewardBase[] GetRewardByLevel(int level)
        {
            return _extraRewardsData[level - 1].Rewards.Select(n => n.Reward).ToArray();
        }
    }

    [Serializable]
    internal struct MissionLevelExtraRewardData
    {
        public MissionExtraRewardData[] Rewards;
    }

    [Serializable]
    internal struct MissionExtraRewardData
    {
        public ExtraRewardBase Reward;
        public int Quantity;
    }
}
