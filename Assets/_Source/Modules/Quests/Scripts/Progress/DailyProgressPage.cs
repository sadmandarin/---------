using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    internal class DailyProgressPage : MonoBehaviour
    {
        internal Action<DailyProgressItemSO> OnItemPressed;

        [SerializeField] private DailyProgressUiItem _itemPrefab;
        [SerializeField] private RectTransform _itemsParent;
        [SerializeField] private DailyProgressPageTip _tip;

        private List<DailyProgressUiItem> _items = new List<DailyProgressUiItem>();

        internal void UpdateItems(List<ProgressItemData> items)
        {
            ClearOldItems();
            foreach (var itemData in items)
            {
                var itemPrefab = Instantiate(_itemPrefab, _itemsParent);
                itemPrefab.SetXPosition(_itemsParent.sizeDelta.x * itemData.Item.ProgressPointsToUnlock / 100);
                itemPrefab.SetUp(itemData.Item, itemData.IsCollected);
                itemPrefab.OnTryingToClaim += ClaimItem;
                itemPrefab.OnRequestingTip += ShowTip;
                _items.Add(itemPrefab);
            }
        }

        private void ClaimItem(DailyProgressItemSO item, DailyProgressUiItem uiItem)
        {
            OnItemPressed?.Invoke(item);
            uiItem.SetUp(item, true);
        }

        private void ShowTip(DailyProgressItemSO item, DailyProgressUiItem uiItem)
        {
            _tip.ToggleTipView(true);
            _tip.SetTextView(item.Reward.BasicDescription, item.Reward.Icon, item.QuantityOfReward);
            _tip.SetTipPosition(uiItem.transform.position);
        }

        private void ClearOldItems()
        {
            foreach (var item in _items)
            {
                Destroy(item.gameObject);
            }

            _items.Clear();
        }
    }
}
