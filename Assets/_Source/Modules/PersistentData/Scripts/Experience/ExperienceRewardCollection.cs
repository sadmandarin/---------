using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Experience/RewardCollection")]
    public class ExperienceRewardCollection : PersistentCollection<ExperienceRewardData>
    {
        public bool IsInitialized => CollectionValue.Count > 0;

        [SerializeField] private ExperienceConfigSO _config;

        public override void InitWithStartingData()
        {
            CollectionValue.Clear();
            for (int i = 0; i < _config.ExperienceLevelList.Count; i++)
            {
                ExperienceLevel item = _config.ExperienceLevelList[i];
                CollectionValue.Add(new ExperienceRewardData(i, false));
            }
        }

        public bool IsCollected(int rewardLevel)
        {
            if (rewardLevel > CollectionValue.Count - 1)
            {
                for (int i = rewardLevel; i < _config.ExperienceLevelList.Count; i++)
                {
                    ExperienceLevel item = _config.ExperienceLevelList[i];
                    CollectionValue.Add(new ExperienceRewardData(i, false));
                }
            }
            return CollectionValue.First(n => n.Level == rewardLevel).IsCollected;
        }

        public void CollectReward(int rewardLevel)
        {
            var rewardData = CollectionValue.First(n => n.Level == rewardLevel);
            var indexOfRewardData = CollectionValue.IndexOf(rewardData);
            CollectionValue[indexOfRewardData] = rewardData.Collected();
            CollectionChanged?.Invoke();
        }
    }
}
