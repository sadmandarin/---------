using UnityEngine;

namespace Quests
{
    internal class TaskNoticeTest : MonoBehaviour
    {
        [SerializeField] QuestItemDescriptionSO _quest;
        [SerializeField] QuestsCompleter _questCompleter;

        [ContextMenu(nameof(Test))]
        internal void Test()
        {
            _questCompleter.CompleteQuest(_quest);
        }
    }
}
