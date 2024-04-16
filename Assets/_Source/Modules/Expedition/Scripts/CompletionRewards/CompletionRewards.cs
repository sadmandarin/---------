using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    internal class CompletionRewards : MonoBehaviour
    {
        [SerializeField] private ConquestLevelsCollection _levelsCollection;
        [SerializeField] private ExtraRewardsBaseConfig _expeditionExtraRewards;
        [SerializeField] private CompletionRewardItem _itemPrefab;
        [SerializeField] private Transform _contentParent;
        [SerializeField] private GameObject[] _objectToTurnOffToDisableRewards;

        internal void SetUp(int level)
        {
            var levelData = _levelsCollection.GetLevelData(level);
            if (levelData.ClaimedReward == true)
            {
                DisableRewardsVisual();
            }
            else
            {
                SetUpRewards(level, out ExtraRewardBase[] extraRewards);
                ClaimRewards(level, extraRewards);
                _levelsCollection.MarkLevelAsClaimedReward(level);
            }

        }

        private void DisableRewardsVisual()
        {
            foreach (var item in _objectToTurnOffToDisableRewards)
            {
                item.SetActive(false);
            }
        }

        private void SetUpRewards(int level, out ExtraRewardBase[] extraRewards)
        {
            extraRewards = _expeditionExtraRewards.GetRewardByLevel(level);
            foreach (var extraReward in extraRewards)
            {
                if (extraReward != null)
                {
                    var quantityOfReward = _expeditionExtraRewards.GetQuantityForReward(extraReward, level);
                    var extraRewardItem = Instantiate(_itemPrefab, _contentParent);
                    extraRewardItem.SetUp(extraReward.Icon, quantityOfReward);
                }
            }
        }

        private void ClaimRewards(int level, ExtraRewardBase[] extraRewards)
        {
            foreach (var extraReward in extraRewards)
            {
                var quantityOfReward = _expeditionExtraRewards.GetQuantityForReward(extraReward, level);
                extraReward.ClaimReward(quantityOfReward);
            }
            PlayerPrefs.SetInt("CollectMoney", 1);
        }
    }
}
