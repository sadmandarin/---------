using PersistentData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "MysticStore/ItemsCollection")]
    public class MysticStoreItemsCollection : PersistentCollection<MysticStoreItemData>
    {

        public void AddNewItem(string guid)
        {
            CollectionValue.Add(new MysticStoreItemData(guid, false));
        }

        public void AddItems(List<string> itemsGuids)
        {
            foreach (string guid in itemsGuids)
            {
                CollectionValue.Add(new MysticStoreItemData(guid, false));
            }
            CollectionChanged?.Invoke();
        }

        public void ClaimItem(string guid)
        {
            if (CollectionValue.Any(n => n.GUID == guid))
            {
                MysticStoreItemData itemThatWillBeUnlocked = CollectionValue.First(n => n.GUID == guid);
                int indexOfItem = CollectionValue.IndexOf(itemThatWillBeUnlocked);
                CollectionValue[indexOfItem] = itemThatWillBeUnlocked.ClaimItem();
                CollectionChanged?.Invoke();
            }
        }
    }
}
