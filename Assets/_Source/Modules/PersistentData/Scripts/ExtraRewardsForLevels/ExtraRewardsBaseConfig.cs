using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    public abstract class ExtraRewardsBaseConfig : ScriptableObject
    {
        public abstract ExtraRewardBase[] GetRewardByLevel(int level);
        public abstract int GetQuantityForReward(ExtraRewardBase reward, int level);
    }
}
