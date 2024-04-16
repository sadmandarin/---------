using PersistentData;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    internal class QuestsDialogButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private QuestsDialog _questsDialog;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _redDot;
        [SerializeField] private Text _numberOfQuestsCompletedText;
        [SerializeField] private CompletedQuestsCollection _quests;
        [SerializeField] private AvailableQuests _allQuests;
        [SerializeField] private QuestsCompleter _questsCompleter;

        private void OnEnable()
        {
            _button.onClick.AddListener(OpenDialog);
            _questsCompleter.OnQuestProgressIncreased += HandleQuestProgressIncreased;
            _questsCompleter.OnQuestRewardClaimed += UpdateVisual;
            UpdateVisual();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OpenDialog); 
            _questsCompleter.OnQuestProgressIncreased -= HandleQuestProgressIncreased;
            _questsCompleter.OnQuestRewardClaimed -= UpdateVisual;
        }

        private void HandleQuestProgressIncreased(QuestItemDescriptionSO sO, int arg2)
        {
            if (this != null && gameObject != null && gameObject.activeInHierarchy)
                UpdateVisual();
        }

        private void OpenDialog()
        {
            var dialog = Instantiate(_questsDialog);
            dialog.InitCamera(_canvas.worldCamera);
        }

        private void UpdateVisual()
        {
            if (this == null || gameObject == null || gameObject.activeInHierarchy == false)
                return;
            
            int numberOfQuestsCompleted = 0;

            foreach (var quest in _allQuests.Quests)
            {
                int index = _allQuests.GetIndexOfQuestInList(quest);
                if (_quests.IsQuestInCollection(index))
                {
                    var timesCompleted = _quests.GetTimesAQuestIsCompleted(index);
                    bool isFullyCompleted = timesCompleted >= quest.CompletionRequirement;
                    if (isFullyCompleted && _quests.IsQuestCollected(index) == false)
                        numberOfQuestsCompleted++;
                }
            }
            _redDot.SetActive(numberOfQuestsCompleted > 0);
            if (numberOfQuestsCompleted > 0)
               _numberOfQuestsCompletedText.text = numberOfQuestsCompleted.ToString();
        }

    }
}
