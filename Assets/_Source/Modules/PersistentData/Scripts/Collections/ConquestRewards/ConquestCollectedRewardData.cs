using System;

namespace PersistentData
{
    [Serializable]
    public struct ConquestCollectedRewardData
    {
        public bool NormalRewardCollected;
        public bool VipRewardCollected;

        public ConquestCollectedRewardData(bool normalRewardCollected, bool vipRewardCollected)
        {
            NormalRewardCollected = normalRewardCollected;
            VipRewardCollected = vipRewardCollected;
        }

        public ConquestCollectedRewardData CollectNormalReward()
        {
            return new ConquestCollectedRewardData(true, this.VipRewardCollected);
        }

        public ConquestCollectedRewardData CollectVipReward()
        {
            return new ConquestCollectedRewardData(this.NormalRewardCollected, true);
        }
    }
}
