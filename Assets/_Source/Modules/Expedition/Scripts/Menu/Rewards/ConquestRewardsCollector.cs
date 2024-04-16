using PersistentData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    internal class ConquestRewardsCollector : MonoBehaviour
    {
        [SerializeField] private CommonRewardDialog _commonRewardDialog;
        [SerializeField] private MoneyCollectorController _collector;
        [SerializeField] private ExtraRewardBase _coinsReward, _gemsReward;

        internal void Init(List<ConquestRewardItem> items)
        {
            foreach (ConquestRewardItem item in items)
            {
                item.OnRewardClaimed -= HandleRewardClaimed;
                item.OnRewardClaimed += HandleRewardClaimed;
            }
        }

        private void HandleRewardClaimed(ExtraRewardBase reward, int quantity)
        {
            var dialog = Instantiate(_commonRewardDialog);
            dialog.SetView(reward, quantity);
            dialog.Animate();
            dialog.OnDialogClosed += HandleDialogClosed;
        }

        private void HandleDialogClosed(ExtraRewardBase reward)
        {
            if (reward == _coinsReward)
            {
                _collector.CollectMoney();
                return;
            }
            if (reward == _gemsReward)
            {
                _collector.CollectGems();
            }
        }
    }
}
