using PersistentData;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quests
{
    [CreateAssetMenu(menuName = "Quests/Config")]
    public class QuestsCompleter : ScriptableObject
    {
        internal Action<QuestItemDescriptionSO, int> OnQuestProgressIncreased;
        public Action OnQuestRewardClaimed;

        [SerializeField] private CompletedQuestsCollection _questsCollection;
        [SerializeField] private AvailableQuests _availableQuests;

        public void CompleteQuest(QuestItemDescriptionSO quest)
        {
            if (_questsCollection.CollectionValue.Count == 0)
                UpdateQuests();

            int indexOfQuestInList = _availableQuests.GetIndexOfQuestInList(quest);
            if (indexOfQuestInList == -1)
            {
                Debug.LogError("Quest Not Found In Predefined List");
                return;
            }
            if (_questsCollection.IsQuestInCollection(indexOfQuestInList) == false) 
            {
                Debug.Log("Quest Not In Collection For Daily Quests");
                return;
            }
            _questsCollection.CompleteQuest(indexOfQuestInList);
            int timesCompleted = _questsCollection.GetTimesAQuestIsCompleted(indexOfQuestInList);
            if (timesCompleted > quest.CompletionRequirement)
                return;
            OnQuestProgressIncreased(quest, timesCompleted);
        }

        internal void UpdateQuests()
        {
            _questsCollection.ReinitializeQuestsData(_availableQuests.GetNotRepeatingQuestsIndexes(_questsCollection.QuestsPerDay));
        }

        public void ClaimRewardForQuest(QuestItemDescriptionSO quest)
        {
            int indexOfQuestInList = _availableQuests.GetIndexOfQuestInList(quest);
            if (indexOfQuestInList == -1)
            {
                Debug.LogError("Quest Not Found In Predefined List");
                return;
            }
            if (_questsCollection.IsQuestInCollection(indexOfQuestInList) == false)
            {
                Debug.Log("Quest Not In Collection For Daily Quests");
                return;
            }

            _questsCollection.ClaimRewardForQuest(indexOfQuestInList);

            foreach (var reward in quest.Reward.ExtraRewards)
            {
                reward.ExtraReward.ClaimReward(reward.Quantity);
            }
            OnQuestRewardClaimed?.Invoke();
        }

        public void ClaimRewardForQuests(List<QuestItemDescriptionSO> questDescriptions)
        {
            List<(ExtraRewardBase, int)> extraRewards = new List<(ExtraRewardBase, int)>();

            foreach (var questDescription in questDescriptions)
            {
                int indexOfQuestInList = _availableQuests.GetIndexOfQuestInList(questDescription);
                _questsCollection.ClaimRewardForQuest(indexOfQuestInList);

                foreach (var reward in questDescription.Reward.ExtraRewards)
                {
                    if (extraRewards.Any(n => n.Item1 == reward.ExtraReward))
                    {
                        var item = extraRewards.First(n => n.Item1 == reward.ExtraReward);
                        item.Item2 += reward.Quantity;
                    }
                    else
                    {
                        extraRewards.Add((reward.ExtraReward, reward.Quantity));
                    }
                }

                foreach (var rewardTuple in extraRewards)
                {
                    rewardTuple.Item1.ClaimReward(rewardTuple.Item2);
                }
            }

            OnQuestRewardClaimed?.Invoke();
        }
    }
}
