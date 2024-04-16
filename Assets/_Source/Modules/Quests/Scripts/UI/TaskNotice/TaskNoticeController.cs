using UnityEngine;

namespace Quests
{
    internal class TaskNoticeController : MonoBehaviour
    {
        [SerializeField] private TaskNoticeView _view;
        [SerializeField] private QuestsCompleter _questsCompleter;

        private void OnEnable()
        {
            _questsCompleter.OnQuestProgressIncreased += ShowNotice;
            _view.ResetVisual();
        }

        private void OnDisable()
        {
            _questsCompleter.OnQuestProgressIncreased -= ShowNotice;
        }

        private void ShowNotice(QuestItemDescriptionSO quest, int timesCompleted)
        {
            _view.SetUp(timesCompleted, quest.CompletionRequirement, quest.Title());
            _view.DoAnimation();
        }
    }
}
