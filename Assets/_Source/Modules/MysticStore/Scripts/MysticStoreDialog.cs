using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MysticStore
{
    internal class MysticStoreDialog : MonoBehaviour
    {
        [SerializeField] private MysticStoreItemUpdater _itemUpdater;
        [SerializeField] private MysticStoreDetailsDialog _detailsDialog;
        [SerializeField] private MysticStoreItemsCollection _itemCollection;
        [SerializeField] private GameObject _instantPurchaseGems;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private FloatVariableSO _gems;

        private void Awake()
        {
            _itemUpdater.LoadItems();
            AssignEventListeners();
        }

        internal void AssignEventListeners()
        {
            foreach (var shelfItem in _itemUpdater.ShelfItems)
            {
                shelfItem.OnPressed += SpawnDialog;
            }
        }

        private void SpawnDialog(MysticRewardItemBase item)
        {
            if (item.Price() >= _gems.Value)
            {
                Instantiate(_instantPurchaseGems);
            }
            else
            {
                var dialog = Instantiate(_detailsDialog);
                dialog.Init(item, _canvas.worldCamera);
                dialog.OnItemPurchased += HandleItemPurchased;
            }
            
        }

        private void HandleItemPurchased(MysticRewardItemBase item)
        {
            var shelfItem = _itemUpdater.ShelfItems.FirstOrDefault(n => n.RewardItem == item);
            if (shelfItem != null)
            {
                shelfItem.MakeSoldOut();
                _itemCollection.ClaimItem(item.GUID);
            }
        }
    }
}
