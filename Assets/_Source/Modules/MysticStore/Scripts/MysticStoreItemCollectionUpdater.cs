using PersistentData;
using System.Collections.Generic;
using UnityEngine;

namespace MysticStore
{
    [CreateAssetMenu(menuName = "MysticStore/ItemUpdater")]
    internal class MysticStoreItemCollectionUpdater : ScriptableObject
    {
        [SerializeField] private MysticStoreRewardsConfig _config;
        [SerializeField] private MysticStoreItemsCollection _itemsCollections;

        internal void RefreshItems()
        {
            _itemsCollections.InitWithStartingData();
            var randomItems = _config.GetRandomItems();
            var randomItemsGuid = new List<string>();
            for (int i = 0; i < randomItems.Count; i++)
            {
                var randomItem = randomItems[i];
                randomItemsGuid.Add(randomItem.GUID);
            }
            _itemsCollections.AddItems(randomItemsGuid);
        }
    }

}
