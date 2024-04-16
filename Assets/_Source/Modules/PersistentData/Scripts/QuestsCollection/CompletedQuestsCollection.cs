using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Quests/Collection")]
    public class CompletedQuestsCollection : PersistentCollection<QuestData>
    {
        public int QuestsPerDay => _questsPerDay;

        [SerializeField] private int _questsPerDay = 6;

        public bool IsQuestInCollection(int indexOfQuest) => CollectionValue.Any(n => n.IndexOfQuest == indexOfQuest);
        public int GetTimesAQuestIsCompleted(int indexOfQuest) => CollectionValue.First(n => n.IndexOfQuest == indexOfQuest).TimesCompleted;

        public void CompleteQuest(int indexOfQuest)
        {
            var questData = CollectionValue.First(n => n.IndexOfQuest == indexOfQuest);
            var indexOfQuestData = CollectionValue.IndexOf(questData);
            CollectionValue[indexOfQuestData] = CollectionValue[indexOfQuestData].CompleteQuest();
            CollectionChanged?.Invoke();
        }

        public void ClaimRewardForQuest(int indexOfQuest)
        {
            var questData = CollectionValue.First(n => n.IndexOfQuest == indexOfQuest);
            var indexOfQuestData = CollectionValue.IndexOf(questData);
            CollectionValue[indexOfQuestData] = CollectionValue[indexOfQuestData].ClaimReward();
            CollectionChanged?.Invoke();
        }

        public void ReinitializeQuestsData(List<int> indexesOfQuests)
        {
            CollectionValue.Clear();
            foreach (var questIndex in indexesOfQuests)
            {
                CollectionValue.Add(new QuestData(questIndex, 0, false));           
            }
            CollectionChanged?.Invoke();
        }

        public bool IsQuestCollected(int index) => CollectionValue.First(n => n.IndexOfQuest == index).ClaimedReward;
    }
}
