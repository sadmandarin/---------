using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Expedition/Collection")]
    public class ConquestLevelsCollection : PersistentCollection<ConquestLevelsData>
    {
        public int StarsCollected(int from, int to) => CollectionValue.Where(n => n.Level >= from && n.Level <= to).Sum(n => n.StarsFilled);

        public void SetUpStartingData()
        {
            CollectionValue.Clear();
            for (int i = 1; i <= 40; i++)
            {
                if (i % 20 == 1)
                    CollectionValue.Add(new ConquestLevelsData(i, 0, true, false, false));
                else
                    CollectionValue.Add(new ConquestLevelsData(i, 0, false, false, false));
            }
        }

        public ConquestLevelsData GetLevelData(int level)
        {
            CheckIfLevelInCollection(level);

            return CollectionValue.First(n => n.Level == level);
        }

        public void FinishLevel(int level, int stars)
        {
            CheckIfLevelInCollection(level);

            var levelData = CollectionValue.FirstOrDefault(n => n.Level == level);
            int indexOfLevelInCollection = CollectionValue.IndexOf(levelData);
            CollectionValue[indexOfLevelInCollection] = levelData.FinsishLevel(stars);

            int maxLevel = CollectionValue.Max(n => n.Level);

            if (level < maxLevel)
            {
                UnlockLevel(level + 1);
            }

            CollectionChanged?.Invoke();
        }

        public void UnlockLevel(int levelToUnlock)
        {
            CheckIfLevelInCollection(levelToUnlock);

            var levelData = CollectionValue.FirstOrDefault(n => n.Level == levelToUnlock);
            int indexOfLevelInCollection = CollectionValue.IndexOf(levelData);
            CollectionValue[indexOfLevelInCollection] = levelData.Unlock();
            CollectionChanged?.Invoke();
        }

        public void MarkLevelAsClaimedReward(int level)
        {
            CheckIfLevelInCollection(level);

            var levelData = CollectionValue.FirstOrDefault(n => n.Level == level);
            int indexOfLevelInCollection = CollectionValue.IndexOf(levelData);
            CollectionValue[indexOfLevelInCollection] = levelData.ClaimReward();
            CollectionChanged?.Invoke();
        }

        private void CheckIfLevelInCollection(int level)
        {
            if (CollectionValue.Any(n => n.Level == level) == false)
            {
                CollectionValue.Add(new ConquestLevelsData(level, 0, level % 20 == 1, false, false));
            }
        }

        public bool HasFinishedLevel(int level)
        {
            if (level == 0)
                return true;
            if (level > CollectionValue.Count)
                return false;
            return CollectionValue.First(n => n.Level == level).Finished;
        }
    }
}
