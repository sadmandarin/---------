using System;

namespace PersistentData
{
    [Serializable]
    public struct ExperienceLevel
    {
        public int ExperienceToNextLevel;
        public ExtraRewardBase Reward;
        public int QuantityOfReward;
    }
}
