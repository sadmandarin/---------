using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quests
{
    [CreateAssetMenu(menuName = "Quests/AllMissions")]
    internal class AvailableQuests : ScriptableObject
    {
        internal List<QuestItemDescriptionSO> Quests => _availableQuests;

        [SerializeField] private List<QuestItemDescriptionSO> _availableQuests;

        internal int GetIndexOfQuestInList(QuestItemDescriptionSO quest) => _availableQuests.IndexOf(quest);

        internal List<int> GetNotRepeatingQuestsIndexes(int numberOfQuests)
        {
            List<int> result = new List<int>();

            System.Random random = new System.Random();
            List<QuestItemDescriptionSO> shuffledList = _availableQuests.OrderBy(x => random.Next()).ToList();

            // Add the items to the result list one by one until we have 9 non-repeating items
            foreach (var item in shuffledList)
            {
                var index = GetIndexOfQuestInList(item);
                if (!result.Contains(index))
                {
                    result.Add(index);

                    if (result.Count == numberOfQuests)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        internal QuestItemDescriptionSO GetQuestByIndex(int indexOfQuest)
        {
            if (indexOfQuest > _availableQuests.Count || indexOfQuest < 0)
            {
                Debug.LogError("Wrong index to search for quest");
                indexOfQuest = Mathf.Clamp(indexOfQuest, 0, _availableQuests.Count - 1);
            }
            
            return _availableQuests[indexOfQuest];
        }
    }
}
