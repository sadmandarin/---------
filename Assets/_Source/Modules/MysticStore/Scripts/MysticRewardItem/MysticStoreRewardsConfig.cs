using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace MysticStore
{
    [CreateAssetMenu(menuName = "MysticStore/ItemConfig")]
    internal class MysticStoreRewardsConfig : ScriptableObject
    {
        [SerializeField] private List<MysticRewardItemBase> _items;

        internal List<MysticRewardItemBase> GetRandomItems()
        {
            List<MysticRewardItemBase> result = new List<MysticRewardItemBase>();

            System.Random random = new System.Random();
            List<MysticRewardItemBase> shuffledList = _items.OrderBy(x => random.Next()).ToList();

            // Add the items to the result list one by one until we have 9 non-repeating items
            foreach (MysticRewardItemBase item in shuffledList)
            {
                if (!result.Contains(item))
                {
                    result.Add(item);

                    // Break the loop if we have 9 non-repeating items
                    if (result.Count == 9)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        internal MysticRewardItemBase GetItemByGUID(string guid)
        {
            return _items.FirstOrDefault(n => n.GUID == guid);
        }
    }
}
