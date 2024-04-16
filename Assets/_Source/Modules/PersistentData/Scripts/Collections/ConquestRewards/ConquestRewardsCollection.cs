using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Expedition/RewardsCollected")]
    public class ConquestRewardsCollection : PersistentCollection<ConquestCollectedRewardData>
    {
        public ConquestCollectedRewardData GetRewardData(int index)
        {
            TryFillMissingItems(index);
            return CollectionValue[index];
        }

        public void SetUpStartingData(int numberOfRewards)
        {
            for (int i = 0; i < numberOfRewards; i++)
            {
                CollectionValue.Add(new ConquestCollectedRewardData(false, false));
            }
            CollectionChanged?.Invoke();
        }

        public void CollectReward(int levelOfReward, bool collectNormal)
        {
            TryFillMissingItems(levelOfReward);

            var collectedData = CollectionValue[levelOfReward];
            CollectionValue[levelOfReward] = collectNormal ? collectedData.CollectNormalReward() : collectedData.CollectVipReward();
            CollectionChanged?.Invoke();
        }

        private void TryFillMissingItems(int levelOfReward)
        {
            if (CollectionValue.Count < levelOfReward + 1)
            {
                for (int i = 0; i < levelOfReward + 1 - CollectionValue.Count; i++)
                {
                    CollectionValue.Add(new ConquestCollectedRewardData(false, false));
                }
            }
        }
    }
}
