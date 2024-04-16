using PersistentData;
using System;

namespace Expedition
{
    [Serializable]
    internal struct ConquestRewardLevelData
    {
        public int StarsToGet;
        public ExtraRewardBase NormalReward;
        public int NormalQuantity;
        public ExtraRewardBase VipReward;
        public int VipQuantity;
    }
}
