using PersistentData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    internal class DailyProgressItemsController : MonoBehaviour
    {
        [SerializeField] private DailyProgressCollection _progressCollection;
        [SerializeField] private List<DailyProgressItemSO> _dailyProgressItemDatas;
        [SerializeField] private DailyProgressPage _progressPage;
        [SerializeField] private ProgressRewardDetail _rewardDialog;

        private void OnEnable()
        {
            UpdateItems();
            _progressPage.OnItemPressed += HandleItemPressed;
        }

        private void HandleItemPressed(DailyProgressItemSO item)
        {
            _progressCollection.CollectReward(_dailyProgressItemDatas.IndexOf(item));
            var dialog = Instantiate(_rewardDialog);
            dialog.SetUp(item.Reward, item.QuantityOfReward);
        }

        internal void UpdateItems()
        {
            if (_progressCollection.CollectionValue.Count == 0)
            {
                _progressCollection.SetStartingData(_dailyProgressItemDatas.Count);
            }

            List<ProgressItemData> progressItems = new List<ProgressItemData>();
            for (int i = 0; i < _progressCollection.CollectionValue.Count; i++)
            {
                progressItems.Add(new ProgressItemData() { Item = _dailyProgressItemDatas[i],
                    IsCollected = _progressCollection.CollectionValue[i].IsCollected });
            }

            _progressPage.UpdateItems(progressItems);
        }
    }

}
