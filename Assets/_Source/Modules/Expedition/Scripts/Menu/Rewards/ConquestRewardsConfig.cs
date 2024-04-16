using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    [CreateAssetMenu(menuName = "Expedition/RewardsConfig")]
    internal class ConquestRewardsConfig : ScriptableObject
    {
        [field:SerializeField] public List<ConquestRewardLevelData> ConquestRewards = new List<ConquestRewardLevelData>();
    }
}
