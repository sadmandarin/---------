using PersistentData;
using UnityEngine;

namespace Quests
{
    internal class QuestDailyActivityAction : DailyActivityActionBase
    {
        [SerializeField] private QuestsCompleter _questsCompleter;
        [SerializeField] private DailyProgressCollection _progressCollection;
        [SerializeField] private IntVariableSO _progressPoints;
        public override void InvokeDailyActivityAction()
        {
            _questsCompleter.UpdateQuests();
            _progressPoints.Value = 0;
            _progressCollection.InitWithStartingData();
        }
    }
}
