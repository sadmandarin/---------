using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Quests/DailyProgressCollection")]
    public class DailyProgressCollection : PersistentCollection<DailyProgressCollectedData>
    {
        public void SetStartingData(int numberOfItems)
        {
            for (int i = 0; i < numberOfItems; i++)
            {
                CollectionValue.Add(new DailyProgressCollectedData(i, false));
            }
        }

        public void CollectReward(int index)
        {
            CollectionValue[index] = CollectionValue[index].CollectReward();
        }

    }

    [Serializable]
    public struct DailyProgressCollectedData
    {
        public int Index;
        public bool IsCollected;

        public DailyProgressCollectedData(int index, bool collected)
        {
            Index = index;
            IsCollected = collected;
        }

        public DailyProgressCollectedData CollectReward()
        {
            return new DailyProgressCollectedData(Index, true);
        }
    }
}
