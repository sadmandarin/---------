using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Shop/DailyItemsCollection")]
    public class DailyTroopsCollection : PersistentCollection<DailyTroopData>
    {
        public void AddTroops(List<string> troopNames)
        {
            CollectionValue.Clear();
            foreach (string name in troopNames)
            {
                CollectionValue.Add(new DailyTroopData { Name = name });
            }
            CollectionChanged?.Invoke();
        }
    }
}
