using PersistentData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MysticStore
{
    internal class MysticStoreItemUpdater : MonoBehaviour
    {
        internal List<MysticStoreShelfItem> ShelfItems => _shelfItems;

        [SerializeField] private Transform _itemsParent;
        [SerializeField] private MysticStoreShelfItem _shelfItemPrefab;
        [SerializeField] private MysticStoreRewardsConfig _config;
        [SerializeField] private MysticStoreItemsCollection _itemsCollections;
        [SerializeField] private MysticStoreItemCollectionUpdater _itemUpdater;
        [SerializeField] private MysticStoreDialog _storeDialog;

        private List<MysticStoreShelfItem> _shelfItems = new List<MysticStoreShelfItem>();

        internal void LoadItems()
        {
            RefreshShelfItemsUI();
            TryLoadItems(out bool loaded);
            if (loaded == false)
                InstantiateNewItems();
        }

        internal void UpdateItems()
        {
            ClearOldItems();
            InstantiateNewItems();
            _storeDialog.AssignEventListeners();
        }

        private void TryLoadItems(out bool loaded)
        {
            if (_itemsCollections.CollectionValue.Count > 0)
            {
                for (int i = 0; i < _itemsCollections.CollectionValue.Count; i++)
                {
                    var loadedData = _itemsCollections.CollectionValue[i];
                    var shelfItem = Instantiate(_shelfItemPrefab, _itemsParent);
                    var rewardItem = _config.GetItemByGUID(loadedData.GUID);
                    shelfItem.Init(rewardItem, loadedData.AlreadyClaimed);
                    _shelfItems.Add(shelfItem);
                }
                loaded = true;
            }
            else
            {
                loaded = false;
            }
        }

        private void InstantiateNewItems()
        {
            _itemUpdater.RefreshItems();
            TryLoadItems(out bool loaded);
        }

        private void ClearOldItems()
        {
            RefreshShelfItemsUI();
            _itemsCollections.InitWithStartingData();
        }

        private void RefreshShelfItemsUI()
        {
            for (int i = 0; i < _shelfItems.Count; i++)
            {
                Destroy(_shelfItems[i].gameObject);
            }
            _shelfItems.Clear();
        }
    }

}
