using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class GameEndRewardArea : MonoBehaviour
    {
        [SerializeField] private Transform _parentToSpawnRewards;
        [SerializeField] private GameEndRewardItem _rewardItemPrefab;
        [SerializeField] private ExtraRewardFloatVariable _coinsExtraReward;

        internal void Init(int moneyForKillingTroops, LevelConfigBaseSO levelConfig, bool hasWon, out int totalMoney)
        {
            totalMoney = moneyForKillingTroops;
            bool alreadyAddedMoneyReward = false;

            if (hasWon)
            {
                SetUpExtraRewards(moneyForKillingTroops, levelConfig, ref totalMoney, ref alreadyAddedMoneyReward);
            }

            if (!alreadyAddedMoneyReward)
            {
                var rewardItem = Instantiate(_rewardItemPrefab, _parentToSpawnRewards);
                rewardItem.SetUp(_coinsExtraReward.Icon, moneyForKillingTroops);
            }
            
        }

        private void SetUpExtraRewards(int moneyForKillingTroops, LevelConfigBaseSO levelConfig, ref int totalMoney, ref bool alreadyAddedMoneyReward)
        {
            int currentLevel = levelConfig.CurrentLevel;
            var extraRewards = levelConfig.ExtraRewards.GetRewardByLevel(currentLevel);
            foreach (var extraReward in extraRewards)
            {
                if (extraReward != null)
                {
                    var quantityOfReward = levelConfig.ExtraRewards.GetQuantityForReward(extraReward, currentLevel);
                    if (extraReward == _coinsExtraReward)
                    {
                        var totalMoneyWon = moneyForKillingTroops + quantityOfReward;
                        var moneyRewardItem = Instantiate(_rewardItemPrefab, _parentToSpawnRewards);
                        moneyRewardItem.SetUp(extraReward.Icon, totalMoneyWon);
                        totalMoney = totalMoneyWon;
                        alreadyAddedMoneyReward = true;
                    }
                    else
                    {
                        var extraRewardItem = Instantiate(_rewardItemPrefab, _parentToSpawnRewards);
                        extraRewardItem.SetUp(extraReward.Icon, quantityOfReward);
                        extraReward.ClaimReward(quantityOfReward);
                    }
                }
            }
        }
    }
}
