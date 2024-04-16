using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    internal class QuestsDialog : MonoBehaviour
    {
        [SerializeField] private Transform _contentParent;
        [SerializeField] private QuestItem _questItemPrefab;
        [SerializeField] private CompletedQuestsCollection _questsCollection;
        [SerializeField] private AvailableQuests _availableQuests;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private QuestsCompleter _questsCompleter;
        [SerializeField] private QuestsEventsRoot _eventsRoot;
        [SerializeField] private IntVariableSO _progressPoints;

        private List<QuestItem> _questItems = new List<QuestItem>();

        internal void InitCamera(Camera canvasCamera)
        {
            _canvas.worldCamera = canvasCamera;
        }

        private void OnEnable()
        {
            SetUp();
            _eventsRoot.InvokeDialogOpened();
            _questsCollection.CollectionChanged += SetUp;
        }

        private void OnDisable()
        {
            _eventsRoot.InvokeDialogClosed();
            _questsCollection.CollectionChanged -= SetUp;
        }

        private void UpdateQuests()
        {
            _questsCollection.ReinitializeQuestsData(_availableQuests.GetNotRepeatingQuestsIndexes(_questsCollection.QuestsPerDay));
            // Update Timer ?
        }

        internal void SetUp()
        {
            ClearQuestItems();

            if (_questsCollection.CollectionValue.Count == 0)
            {
                UpdateQuests();
            }
            foreach (var questData in _questsCollection.CollectionValue)
            {
                var questItem = Instantiate(_questItemPrefab, _contentParent);
                QuestItemDescriptionSO questDescription = _availableQuests.GetQuestByIndex(questData.IndexOfQuest);
                questItem.SetUp(questDescription, questData.TimesCompleted, questData.ClaimedReward);
                _questItems.Add(questItem);
                questItem.OnClaimedReward += HandleOnClaimedReward;
                questItem.OnStartedQuest += HandleQuestStart;
            }

            SortAllQuests();
        }

        private void HandleQuestStart(QuestItemDescriptionSO quest)
        {
            Destroy(gameObject);
            quest.MovingTrigger.RaiseEvent();
        }

        private void ClearQuestItems()
        {
            foreach (var item in _questItems)
            {
                Destroy(item.gameObject);
            }
            _questItems.Clear();
        }

        private void HandleOnClaimedReward(QuestItemDescriptionSO quest)
        {
            List<QuestItemDescriptionSO> questItemDescriptions = new List<QuestItemDescriptionSO>();

            foreach (var questItem in _questItems)
            {
                if (questItem.IsReadyToBeCollected && questItem.Collected == false)
                {
                    questItem.ChangeViewToClaimed();
                    QuestItemDescriptionSO questDescription = questItem.QuestDescription;
                    questItemDescriptions.Add(questDescription);
                }
            }

            _questsCompleter.ClaimRewardForQuests(questItemDescriptions);

            foreach (var questDescription in questItemDescriptions)
            {
                //_questsCompleter.ClaimRewardForQuest(questDescription);
                _progressPoints.Value += questDescription.ProgressQuantity;
            }

            SortAllQuests();
        }

        private void SortAllQuests()
        {
            foreach (var questItem in _questItems)
            {
                if (questItem.IsReadyToBeCollected)
                    questItem.transform.SetAsFirstSibling();
                if (questItem.Collected)
                    questItem.transform.SetAsLastSibling();
            }
        }
    }
}
